import { useState } from "react";
import { IToDoProp } from "../props/IToDoProp";

export function ToDoForm({todo}: IToDoProp){
    const [title, setTitle] = useState(todo.title);
    const [description, setDescription] = useState(todo.description);
    const [lastDay, setLastDay] = useState(todo.lastDay);
    const [isActive, setIsActive] = useState(todo.isActive);
    return(
        <form>
            <label>Название</label>
            <input type="text" value={title} onChange={e => setTitle(e.target.value)} />
            <br/>
            <label>Описание</label>
            <input type="text" value={description} onChange={e => setDescription(e.target.value)} />
            <br/>
            <label>Дата окончания</label>
            <input type="datetime" value={Intl.DateTimeFormat("ru", {dateStyle: "short", timeStyle: "short"}).format(new Date(lastDay))} onChange={e => setLastDay(e.target.value)} />
            <br/>
            <label>Выполено</label>
            <input type="checkbox" checked={isActive} onChange={e => setIsActive(e.target.checked)} />
        </form>
    )
}