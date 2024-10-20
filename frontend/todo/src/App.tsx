import { Route, Link, Routes } from 'react-router-dom';
import { MyToDosPage } from './pages/MyToDosPage';
import { TodoDetailPage } from './pages/ToDoDetailPage';
import { CreateToDoPage } from './pages/CreateToDoPage';
import './styles/App.css';


function App() {
  return (
    <nav>
      <ul>
        <li>
          <Link to="/mytodo">Мои задачи</Link>
        </li>
        <li>
          <Link to="/createtodo">Добавить задачу</Link>
        </li>
      </ul>
      <Routes>
        <Route path="/mytodo" element={<MyToDosPage />} />
        <Route path="/createtodo" element={<CreateToDoPage />} />
        <Route path="/mytodo/:id" element={<TodoDetailPage />} />
      </Routes>
    </nav>
  );
}


export default App;