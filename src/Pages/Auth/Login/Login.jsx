import React from 'react'
import { Link } from 'react-router'
import { useContext, useRef } from 'react';
import AuthContext from '../../../AuthContext/AuthContext';
const Login = () => {
    const {handleSubmit} = useContext(AuthContext);
    const username= useRef('');
    const password= useRef('');

    const handleLogin= async(e) =>{
        e.preventDefault();
        if(username.current.value.length !=0 && 0!= password.current.value.length)
            {
            alert("username:"+username.current.value+" password:"+ password.current.value)
            const body = {
                username : username.current.value,
                password : password.current.value
            }
            const {status, user} = await handleSubmit('/login',body)
            // console.log(status, user)
        }
        else{
            alert("Password and Username cannot be empty!")
        }
    }
  return (
    <div>
        <div className="w-full flex items-center justify-center">
            <div className="lg:container">
                <div className="flex items-center justify-between w-full">
                    <div className="left_wrapper">
                        <img src="/login.png" alt="Login photo missing" />
                    </div>
                    <div className="right_wrapper space-y-4 max-w-[640px] w-full h-auto">
                        <h3 className="text-5xl text-[#313131] font-semibold capitalize">
                            Login
                        </h3>
                        <p className="text-base text-[#313131] font-medium">
                            Access your personal account
                        </p> 
                        <form className="space-y-4" onSubmit={handleLogin}>
                            <input type="text" ref={username} className='max-w-[640px] w-full h-[56px] border-[1px] border-[#313131]
                            rounded-lg outline-0 pl-3'
                            placeholder="Your Email..."
                            />  
                            <input type="password" ref={password}
                            className='max-w-[640px] w-full h-[56px] border-[1px]
                            border-[#313131] rounded-lg outline-0 pl-3
                            ' placeholder='Your Password...'
                            />
                            <button type='submit' className="max-w-[640px] w-full h-[56px] 
                            bg-[#515def] rounded-lg flex items-center justify-center text-base text-[#f3f3f3]
                            capitalize font-semibold bg-sky-500 hover:bg-sky-900
                            ">
                                Log In
                            </button>
                        </form>   
                        
                        <p className="max-w-[640px] w-full h-auto text-sm text-[#313131] font-normal">
                            Not registered yet?
                            <Link to={"/register"} className='font-small flex item-center text-red-500'>
                            Register Now
                            </Link> 
                        </p> 
                        <p className="max-w-[640px] w-full h-auto text-sm text-[#313131] font-normal">
                            <Link to={"/forget-password"} className='font-small flex item-center text-red-500'>
                            Forgot your password
                            </Link> 
                        </p> 
                    </div>
                </div>
            </div>
        </div>
    </div>
  )
}

export default Login