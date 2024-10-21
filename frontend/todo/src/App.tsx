import { Route, Link, Routes, Navigate } from 'react-router-dom';
import { MyToDosPage } from './pages/MyToDosPage';
import { TodoDetailPage } from './pages/ToDoDetailPage';
import { CreateToDoPage } from './pages/CreateToDoPage';
import './styles/App.css';
import { AuthorizationPage } from './pages/AuthorizationPage';
import { RegistrationPage } from './pages/RegistrationPage';


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
        <Route path="/" element={<Navigate to="/authorization" replace />} />
        <Route path='/authorization' element={<AuthorizationPage/>} />
        <Route path='/registration' element={<RegistrationPage/>} />
        <Route path="/mytodo" element={<MyToDosPage />} />
        <Route path="/createtodo" element={<CreateToDoPage />} />
        <Route path="/mytodo/:id" element={<TodoDetailPage />} />
      </Routes>
    </nav>
  );
}


export default App;