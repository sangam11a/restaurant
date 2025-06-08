import React, { useContext, useRef } from 'react';
// import registerImage from "./assets/register.png";
import { Link } from 'react-router';
import AuthContext from '../../../AuthContext/AuthContext';
const Register = () => {
    const {handleSubmit} = useContext(AuthContext);
    const createUsername= useRef('');
    const createPassword= useRef('');
    const createConfirmPassword = useRef('');

    const handleRegister= async(e) =>{
        e.preventDefault();
        if(createPassword.current.value == createConfirmPassword.current.value){
            alert("Password and confirm password are same")
            const body = {
                username : createUsername.current.value,
                password : createPassword.current.value
            }
            const {status, user} = await handleSubmit('/register',body)
        }
        else{
            alert("Password and confirm password differs")
        }
    }

  return (
    <div>
        <div className="w-full flex items-center justify-center">
            <div className="lg:container">
                <div className="flex items-center justify-between w-full">
                    <div className="left_wrapper">
                        <img src="/register.png" alt="registeration photo missing" />
                    </div>
                    <div className="right_wrapper space-y-4 max-w-[640px] w-full h-auto">
                        <h3 className="text-5xl text-[#313131] font-semibold capitalize">
                            Register
                        </h3>
                        <p className="text-base text-[#313131] font-medium">
                            Let's get you all set up so you can access your personal account
                        </p> 
                        <form className="space-y-4" onSubmit={handleRegister}>
                            <input type="text"  className='max-w-[640px] w-full h-[56px] border-[1px] border-[#313131]
                            rounded-lg outline-0 pl-3'
                            placeholder="Your Name..."
                            />  
                            <input ref={createUsername} type="email"  
                            className='max-w-[640px] w-full h-[56px] border-1 border-#[312131] rounded-lg outline-0 pl-3'
                            placeholder="Your email..."
                            />  
                            <input ref={createPassword} type="password" 
                            className='max-w-[640px] w-full h-[56px] border-1 border-#[312121] rounded-lg outline-0 pl-3 '
                            placeholder="Create a new password"
                            />

                            <input ref={createConfirmPassword} type="password"  
                            className='max-w-[640px] w-full h-[56px] border-[1px]
                            border-[#313131] rounded-lg outline-0 pl-3
                            ' placeholder='Confirm password'
                            />
                            <button type='submit' className="max-w-[640px] w-full h-[56px] 
                            bg-[#515def] rounded-lg flex items-center justify-center text-base text-[#f3f3f3]
                            capitalize font-semibold
                            ">
                                Create Account
                            </button>
                        </form>    
                        <p className="max-w-[640px] w-full h-auto text-sm text-[#313131] font-normal">
                            Already Registered
                            <Link to={"/login"} className='font-small flex item-center text-red-500'>
                            Log In
                            </Link> 
                        </p> 
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
  )
}

export default Register