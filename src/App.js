import React from 'react';
import './App.css';
import EmployeeList from './Components/EmployeeList';
import Acreate from './Components/create';


function App() {
  return (
    <div className="container ">
      <EmployeeList/>
      <Acreate/>

    </div>
  );
}

export default App;
