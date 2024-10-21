import axios from 'axios';
import { useEffect, useState } from 'react';
import { ToDoCard } from '../components/ToDoCard';
import { Link } from 'react-router-dom';
import { IToDoProp } from '../props/IToDoProp';
import { IToDo } from '../models/IToDo';

async function fetchData() {
  try 
  {
    const response = await axios.get<IToDo[]>("http://localhost:5237/ToDo");
    return response.data;
  } 
  catch (error)
  {
    console.error("Произошла ошибка", error);
    return [];
  }
}

export function MyToDosPage() {
  const [todos, setToDos] = useState<IToDoProp[]>([]);

  useEffect(() => {
    const getData = async () => {
      const list = await fetchData();
      const formattedList: IToDoProp[] = list.map(todo => ({
        todo
      }));
      setToDos(formattedList);
    };
    getData();
  }, []); 
  
  return(
    <ol>
      {todos.map((todo) => (
        <li key={todo.todo.id}>
          <Link to={`/mytodo/${todo.todo.id}`}>
            <ToDoCard todo={todo.todo} />
          </Link>
        </li>
      ))}
    </ol>
  );
}