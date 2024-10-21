import { FormEvent, useState } from "react";
import axios from "axios";
import Cookies from 'js-cookie';

export function AuthorizationPage() {
    const [message, setMessage] = useState<string>("");
    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    async function onClickHandler(e: FormEvent) {
        e.preventDefault(); 
        const status = await Entry(login, password);
        if (status === 204) {
            setMessage("Вы успешно вошли");
            Cookies.set('login', login, { expires: 1, secure: true });
            window.location.href = "/mytodo";
        } else {
            setMessage("Неверный логин или пароль");
        }
    }

    return (
        <form>
            <label>{message}</label>
            <br/>
            <label>Введите логин</label>
            <br/>
            <input type="text" value={login} onChange={(e) => setLogin(e.target.value)} />
            <br/>
            <label>Введите пароль</label>
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <hr/>
            <button type="submit" onClick={onClickHandler}>Войти</button>
            <button type="submit" onClick={() => {window.location.href = "/registration"}}>Регистрация</button>
        </form>
    );
}

async function Entry(login: string, password: string) {
    const user = { login, password };
    const response = await axios.post("http://localhost:5237/User/verify", user);
    return response.status;
}