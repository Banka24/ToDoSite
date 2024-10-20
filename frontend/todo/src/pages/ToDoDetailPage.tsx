import { FormEvent, useEffect, useState } from "react";
import { IToDo } from "../models/ToDo";
import { IToDoProp } from "../props/IToDoProp";
import { useParams } from "react-router-dom";
import axios from "axios";
import "../styles/ToDoDetail.css";
import moment from "moment-timezone";

function transformToDoData(todoData: IToDo): IToDoProp {
    return {
        todo: todoData,
    };
}

async function fetchData(id: string | undefined) {
    try {
        const response = await axios.get<IToDo>(`http://localhost:5237/ToDo/${id}`);
        return response.data;
    } catch (error) {
        console.error("Ошибка при получении данных:", error);
        return null;
    }
}

export function TodoDetailPage() {
    const { id } = useParams<string>();
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [lastDay, setLastDay] = useState<string>("");
    const [isComplete, setIsComplete] = useState<boolean>(false);
    const [message, setMessage] = useState<string>("");

    useEffect(() => {
        const getData = async () => {
            const todoData = await fetchData(id);
            if (todoData) {
                const transformedData = transformToDoData(todoData);
                setTitle(transformedData.todo.title);
                setDescription(transformedData.todo.description || "");
                setIsComplete(transformedData.todo.isComplete ?? false);
                const localDateTime = moment(transformedData.todo.lastDay).tz(moment.tz.guess()).format("YYYY-MM-DDTHH:mm");
                setLastDay(localDateTime);
            }
        };
        getData();
    }, [id]);

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        const utcLastDay = moment(lastDay).tz(moment.tz.guess()).utc().format();
        const message = await Save(id, title, description, utcLastDay, isComplete);
        setMessage(message);
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>{message}</label>
            <br/>
            <label>Название </label>
            <input type="text" value={title} onChange={e => setTitle(e.target.value)} />
            <br />
            <label>Описание </label>
            <input type="text" value={description} onChange={e => setDescription(e.target.value)} />
            <br />
            <label>Дата окончания</label>
            <input type="datetime-local" value={lastDay} onChange={e => setLastDay(e.target.value)} />
            <br />
            <label>Выполено </label>
            <input type="checkbox" checked={isComplete} onChange={e => setIsComplete(e.target.checked)} />
            <hr />
            <button type="submit">Сохранить</button>
            <button type="button" onClick={async () => setMessage(await Delete(id))}>Удалить</button>
        </form>
    );
}

async function Save(id: string | undefined, title: string, description: string | undefined, lastDay: string, isComplete: boolean): Promise<string> {
    const toDo: IToDo = { title, description, lastDay, isComplete };
    const response = await axios.patch<string>(`http://localhost:5237/ToDo/${id}`, toDo);
    return response.data;
}

async function Delete(id: string | undefined): Promise<string> {
    const response = await axios.delete<string>(`http://localhost:5237/ToDo/${id}`);
    return response.data;
}
