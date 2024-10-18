import { useEffect, useState } from "react";
import { IToDo } from "../models/ToDo";
import { IToDoProp } from "../props/IToDoProp";
import { useParams } from "react-router-dom";
import { ToDoForm } from "../components/ToDoForm";
import axios from "axios";

function transformToDoData(todoData: IToDo): IToDoProp {
    return {
        todo: todoData,
    };
}

async function fetchData(id: string | undefined) {
    console.log(id)
    try {
        const response = await axios.get<IToDo>(`http://localhost:5237/ToDo/${id}`);
        return response.data;
    } catch (error) {
        console.error("Ошибка при получении данных:", error);
        console.log(`http://localhost:5237/ToDo/${id}`)
        return null;
    }
}

export function TodoDetailPage() {
    const [todo, setToDo] = useState<IToDoProp | null>(null);
    const { id } = useParams();
    useEffect(() => {
        const getData = async () => {
            const todoData = await fetchData(id);
            if (todoData !== undefined && todoData !== null) {
                const transformedData = transformToDoData(todoData);
                setToDo(transformedData);
            }
        };
        getData();
    }, [id]); 
    return (
        <div>
            {todo ? <ToDoForm todo={todo.todo} /> : <div>Загрузка...</div>}
        </div>
    );
}
