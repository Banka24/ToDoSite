import { IToDoProp } from '../props/IToDoProp';
import "../styles/ToDoCard.css";
import moment from "moment-timezone";

export function ToDoCard({ todo }: IToDoProp) {
    const lastDayString = moment(todo.lastDay).tz(moment.tz.guess()).format("YYYY-MM-DD HH:mm");

    return (
        <div className="todo-card">
            <h3>{todo.title}</h3>
            <p>{todo.description}</p>
            <hr />
            <label>Последний день: {lastDayString}</label>
            <input type="checkbox" checked={todo.isComplete} readOnly />
        </div>
    );
}