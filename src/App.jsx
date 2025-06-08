import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'
import { BrowserRouter, Routes, Route } from 'react-router'
import Home from './Pages/Home/Home'
import Register from './Pages/Auth/Register/Register'
import ForgetPassword from './Pages/Auth/ForgetPassword/ForgetPassword'
import ResetPassword from './Pages/Auth/ResetPassword/ResetPassword'
import VerifyEmail from './Pages/Auth/VerifyEmail/VerifyEmail'
import Login from './Pages/Auth/Login/Login'

function App() {

  return (
    <>
      <BrowserRouter>
        <Routes>
          <Route path ="/" element={<Home/>}/>
          <Route path = "/register" element= {<Register/>}/>
          <Route path = "/login" element = {<Login/>}/>
          <Route path= "/forget-password" element= {<ForgetPassword/>}/>
          <Route path= "/reset-password" element = {<ResetPassword/>}/>
          <Route path= "/verify-email" element= {<VerifyEmail/>}/>

        </Routes>
      </BrowserRouter>
    </>
  )
}

export default App
