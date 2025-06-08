import React from 'react';
import axios from 'axios';

const axiosInstance = axios.create({
    baseURL:"https://localhost:7240/api/login",
    withCredentials:true,
    headers:{
        'Content-Type':'application/json'
    }
});

const useAxiosInstance = () => {
  return axiosInstance;
}

export default useAxiosInstance