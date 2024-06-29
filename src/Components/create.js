import React, { useState } from "react";
import "./../App.css";
import axios from "axios";

function Acreate() {
    const [formData, setFormData] = useState({
        name: "",
        address: "",
        phoneNumber: "",
        email: ""
      });

      const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({
          ...formData,
          [name]: value
        });
      };

      const handleSubmit = async (e) => {
        e.preventDefault(); // Prevent default form submission
    
        try {
          // Send POST request using Axios
          const response = await axios.post('https://localhost:7002/api/Student', formData, {
            headers: {
              'Content-Type': 'application/json' // Set content type header to JSON
            }
          });

          console.log('Data submitted successfully:', response.data); // Log success message and response data

          // Clear input fields after successful submission
          setFormData({
            name: "",
            address: "",
            phoneNumber: "",
            email: ""
          });
        } catch (error) {
          console.error('Error submitting data:', error.message); // Log error message
        }
      };
    
      return (
        <div className="container">
          <div className="row">
            <div className="col-md-4">
              <h1>Create Student</h1>
              <form onSubmit={handleSubmit}>
                <div className="form-group">
                  <input
                    type="text"
                    name="name"
                    placeholder="Name"
                    className="form-control"
                    value={formData.name}
                    onChange={handleChange}
                    required
                  />
                </div>
                <div className="form-group">
                  <input
                    type="text"
                    name="address"
                    placeholder="Address"
                    className="form-control"
                    value={formData.address}
                    onChange={handleChange}
                  />
                </div>
                <div className="form-group">
                  <input
                    type="text"
                    name="phoneNumber"
                    placeholder="Phone Number"
                    className="form-control"
                    value={formData.phoneNumber}
                    onChange={handleChange}
                  />
                </div>
                <div className="form-group">
                  <input
                    type="email"
                    name="email"
                    placeholder="Email"
                    className="form-control"
                    value={formData.email}
                    onChange={handleChange}
                  />
                </div>
                <div>
                  <input
                    type="submit"
                    value="Submit"
                    className="btn btn-primary"
                  />
                </div>
              </form>
            </div>
          </div>
        </div>
      );
    }
    
    export default Acreate;