import { Route, Link, Routes } from 'react-router-dom';
import { MyToDosPage } from './pages/MyToDosPage';
import { TodoDetailPage } from './pages/ToDoDetailPage';


function App() {
  return (
    <nav>
      <ul>
        <li>
          <Link to="/mytodo">Мои задачи</Link>
        </li>
      </ul>
      <Routes>
        <Route path="/mytodo" element={<MyToDosPage />} />
        <Route path="/mytodo/:id" element={<TodoDetailPage />} />
      </Routes>
    </nav>
  );
}


export default App;
