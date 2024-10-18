import { IToDoProp } from '../props/IToDoProp';
import "../styles/ToDoCard.css"

export function ToDoCard({ todo }: IToDoProp) {
    const lastDayDate = new Date(todo.lastDay);
    const lastDayString = lastDayDate.toUTCString();

    return (
        <div className="todo-card">
            <h3>{todo.title}</h3>
            <p>{todo.description}</p>
            <hr />
            <label>Последний день: {lastDayString}</label>
            <input type="checkbox" checked={todo.isActive} readOnly />
        </div>
    );
}