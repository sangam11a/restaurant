import React from 'react'
import AuthContext from '../AuthContext/AuthContext'
import useAxiosInstance from '../Hooks/useAxiosInstance'


const AuthProvider = ({children}) => {
    const axiosInstance = useAxiosInstance();
    const handleSubmit = async(URL,body)=>{
        try{
            const res = await axiosInstance.post(URL,body);
            console.log(res)
            
            return {
                status:res?.status,
                user: res?.data?.user
            }
        }
        catch(error){
            console.log(error.response.data)
            return {
                status:0,
                user:error.response.data.title
            }
            // console.error(error.message)
        }
    }
  return (
    <AuthContext.Provider value={{handleSubmit}}>
        {children}
    </AuthContext.Provider>
  )
}

export default AuthProvider