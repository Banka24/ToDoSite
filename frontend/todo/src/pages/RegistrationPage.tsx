import { FormEvent, useState } from "react";
import axios from "axios";
import Cookies from 'js-cookie';

export function RegistrationPage() {
    const [message, setMessage] = useState<string>("");
    const [login, setLogin] = useState<string>("");
    const [password, setPassword] = useState<string>("");

    async function onClickHandler(e: FormEvent) {
        e.preventDefault(); 
        const status = await Entry(login, password);
        if (status === 204) {
            setMessage("Вы успешно зарегистрировались");
            Cookies.set('login', login, { expires: 1, secure: true });
            window.location.href = "/mytodo";
        } else {
            setMessage("Произошла ошибка");
        }
    }

    return (
        <form>
            <label>{message}</label>
            <label>Введите логин</label>
            <input type="text" value={login} onChange={(e) => setLogin(e.target.value)} />
            <label>Введите пароль</label>
            <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            <button type="submit" onClick={onClickHandler}>Зарегистрироваться</button>
        </form>
    );
}

async function Entry(login: string, password: string) {
    try{
        const user = { login, password };
        const response = await axios.post("http://localhost:5237/User/registration", user);
        return response.status;
    }
    catch(error){
        console.error(error)
        return 500;
    }
}