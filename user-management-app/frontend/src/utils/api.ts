// This file contains utility functions for making API calls, handling errors, and managing responses.

import axios from 'axios';

// Base URL for the API
const API_BASE_URL = 'http://localhost:5000/api/users'; // Adjust the base URL as needed

// Function to handle API requests
const apiRequest = async (url: string, method: 'GET' | 'POST' | 'PUT' | 'DELETE' = 'GET', data?: any) => {
    try {
        const response = await axios({
            method,
            url: `${API_BASE_URL}${url}`,
            data,
        });
        return response.data; // Return the response data
    } catch (error) {
        // Handle errors
        if (axios.isAxiosError(error)) {
            // If the error is an Axios error, return the error message
            throw new Error(error.response?.data?.message || 'An error occurred while fetching data.');
        } else {
            // For any other errors, return a generic message
            throw new Error('An unexpected error occurred.');
        }
    }
};

// Function to fetch all users with pagination
export const fetchUsers = async (page: number, limit: number) => {
    return await apiRequest(`?page=${page}&limit=${limit}`);
};

// Function to fetch a user by ID
export const fetchUserById = async (id: string) => {
    return await apiRequest(`/${id}`);
};

// Function to search users by a query
export const searchUsers = async (query: string) => {
    return await apiRequest(`/search?query=${query}`);
};