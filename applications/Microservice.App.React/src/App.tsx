import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { MoviesComponent } from './movies/movies.component';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/">
          <Route index element={<MoviesComponent />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;