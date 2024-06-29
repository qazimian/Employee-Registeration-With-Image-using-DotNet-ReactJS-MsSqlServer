import React, { useState } from "react";

const defaultImg = "/img/defaultImg.jpg";

const initialFieldValues = {
  employeeId: 0,
  employeeName: "",
  employeeOccupation: "",
  ImageName: "",
  ImageSrc: defaultImg,
  ImageFile: null,
};

export default function Employee(props) {
  const { addOrEdit } = props;

  const [values, setValues] = useState(initialFieldValues);
  const [errors, setErrors] = useState({});

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setValues({
      ...values,
      [name]: value,
    });
  };

  const showPreview = (e) => {
    if (e.target.files && e.target.files[0]) {
      let ImageFile = e.target.files[0];
      const reader = new FileReader();
      reader.onload = (x) => {
        setValues({
          ...values,
          ImageFile,
          ImageSrc: x.target.result,
        });
      };
      reader.readAsDataURL(ImageFile);
    } 
    else {
      setValues({
        ...values,
        ImageFile: null,
        ImageSrc: defaultImg,
      });
    }
  };
  const validate = () => {
    let temp = {};
    temp.Name = values.Name === "" ? false : true;
    temp.ImageSrc = values.ImageSrc === defaultImg ? false : true;
    setErrors(temp);
    return Object.values(temp).every(x => x === true);
  };

  const resetForm = () => {
    setValues(initialFieldValues);
    document.getElementById("image-uploader").value = null;
    setErrors({})
  };

  const handleFormSubmit = e => {
    e.preventDefault();
    if (validate()) {
      const formData = new FormData();
      formData.append('employeeId', values.employeeId);
      formData.append('employeeName', values.employeeName);
      formData.append('employeeOccupation', values.employeeOccupation);
      formData.append('ImageName', values.ImageName);
      formData.append('ImageFile', values.ImageFile);
      addOrEdit(formData, resetForm);
    }
  };

  const applyErrorClass = field =>
    ((field in errors && errors[field] === false)? 'invalidField' : '');

  return (
    <>
      <div className="container text-center">
        <p>Employee from</p>
      </div>

      <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
        <div className="card">
          {/* ---------- uploaded image showing ------------  */}
          <div className="d-flex justify-content-center mx-2">
            <img
              src={values.ImageSrc}
              alt=""
              className="card-img-top   profileimg"
            />
          </div>

          <div className="card-body ">
            {/*----------- image input tag  -------------*/}
            <div className="form-group my-1 ">
              <input
                type="file"
                accept="image/*"
                className={"form-control-file" + applyErrorClass("ImageSrc")}
                onChange={showPreview}
                id="image-uploader"
              />
            </div>
            {/*----------- Name input tag  -------------*/}
            <div className="form-group my-1 ">
              <input
                type="text"
                className={"form-control" + applyErrorClass('Name')}
                placeholder="Employee Name"
                name="employeeName"
                value={values.employeeName}
                onChange={handleInputChange}
              />
            </div>
            {/*----------- Occupation input tag  -------------*/}
            <div className="form-group my-1">
              <input
                type="text"
                className="form-control"
                placeholder="Employee Occupation"
                name="employeeOccupation"
                value={values.employeeOccupation}
                onChange={handleInputChange}
              />
            </div>
            {/*----------- Submit button  -------------*/}
            <div className="form-group text-center">
              <button type="submit" className="btn btn-primary">
                Submit
              </button>
            </div>
            <div className="form-group"></div>
          </div>
        </div>
      </form>
    </>
  );
}
