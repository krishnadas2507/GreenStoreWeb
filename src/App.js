import './App.css';
import{BrowserRouter as Router, Routes, Route} from 'react-router-dom';
import Layout from './compoents/Layout';
import Loginpage from './compoents/Loginpage';
import Dashboardpage from './compoents/Dashboardpage';
import Locationpage from './compoents/Locationpage';

function App() {
  return (
    <div className="App">
    <Router>
      <Routes>
        <Route path='/' element={<Layout/>}>
          <Route index element={<Loginpage/>}/>
          <Route path='/dashboardpage' element={<Dashboardpage/>}/>
          <Route  path='/locationpage' element={<Locationpage/>}/>
          <Route path="*" element={<div>PageNot found</div>}/>
        </Route>
      </Routes>
    </Router>
    </div>
  );
}

export default App;
