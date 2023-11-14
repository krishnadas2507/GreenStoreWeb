import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Styles/dashboard.css'; 

function DashboardPage() {
  const [vegetables, setVegetables] = useState([]);

  useEffect(() => {
    axios.get('https://localhost:7101/api/User/vegetables')
      .then(response => {
        console.log('API Response:', response.data);
        setVegetables(response.data);
      })
      .catch(error => {
        console.error('Error fetching vegetables:', error);
      });
  }, []);

  console.log('Rendered with vegetables:', vegetables);

  return (
    <div className="dashboard-container">
      <div className='head'><h1>VEGETABLE LIST</h1></div>
      <div className="table-container">
        <table className="table">
          <thead>
            <tr>
              <th>Vegetable Name</th>
              <th>Price</th>
            </tr>
          </thead>
          <tbody>
            {vegetables.map((vegetable, index) => (
              <tr key={index}>
                <td>{vegetable}</td>
                <td><input className='price-input' type="number" /></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
      <div className='cokkies-section'>
        <div className='cokkies'>THE USER COOKIE FOR THIS SECTION IS : </div>
        <div className='location-btn'><button className='map-btn'>Next</button></div>
      </div>
    </div>
  );
}

export default DashboardPage;
