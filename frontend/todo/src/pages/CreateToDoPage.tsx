import { FormEvent, useState } from "react";
import { IToDo } from "../models/ToDo";
import axios from "axios";
import moment from "moment-timezone";

export function CreateToDoPage() {
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [lastDay, setLastDay] = useState<string>(moment().tz(moment.tz.guess()).format("YYYY-MM-DDTHH:mm"));
    const [message, setMessage] = useState<string>("");

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();
        const utcLastDay = moment(lastDay).tz(moment.tz.guess()).utc().format();
        setMessage(await AddToDo(title, utcLastDay, description));
    };

    return (
        <form onSubmit={handleSubmit}>
            <label>{message}</label>
            <br />
            <label>Название </label>
            <input type="text" value={title} onChange={e => setTitle(e.target.value)} />
            <br />
            <label>Описание </label>
            <input type="text" value={description} onChange={e => setDescription(e.target.value)} />
            <br />
            <label>Дата окончания </label>
            <input type="datetime-local" 
                value={lastDay} 
                onChange={e => setLastDay(e.target.value)} />
            <hr />
            <button type="submit">Добавить</button>
        </form>
    );
}

async function AddToDo(title: string, lastDay: string, description?: string) {
    const toDo: IToDo = { title, description, lastDay };
    try {
        const response = await axios.post<IToDo>("http://localhost:5237/ToDo", toDo);
        if (response.status === 400) {
            return "Ошибка добавления";
        }
        return "Добавлено";
    } catch (error) {
        return "Ошибка добавления: " + error;
    }
}