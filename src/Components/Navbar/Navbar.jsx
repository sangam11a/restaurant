import React from 'react'
import { BiSolidLogInCircle } from 'react-icons/bi'
import { FaRegCircle, FaRegUserCircle } from 'react-icons/fa'
import { Link } from 'react-router'

const Navbar = () => {
  return (
    <>
    <div className="w-full h-[80px] vorder-b-[1px] border-[#313131]">
        <div className="lg:container mx-auto flex items-center justify-center">
            <div className="flex items-center justify-between w-full h-full">
                <div className="logo">
                    <Link to={'/'} className="flex items-center gap-3 text-4xl text-[#313131] capitalize">
                        <FaRegUserCircle color="#515def" size="2rem">Auth System</FaRegUserCircle>
                        {/* <FaRegUserCircle/> */}
                    </Link>
                </div>
                <div>
                    <Link to={"/login"} className="text-xl text-[#313131] capitalize font-medium flex items-center gap-3 border-b-[2px] border-[#515def]">
                        <BiSolidLogInCircle color="#515def" size="2rem"/>
                        Login
                    </Link>
                    <Link to={"/register"} className="text-xl text-[#313131] capitalize font-medium flex items-center gap-3 border-b-[2px] border-[#515def]">
                        <BiSolidLogInCircle color="#515def" size="2rem"/>
                        Register
                    </Link>
                </div>
            </div>
        </div>

    </div>
    </>
  )
}

export default Navbar