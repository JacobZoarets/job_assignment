// This file contains functions for interacting with the backend API to fetch user data.

import axios from 'axios';
import { User, PaginatedResult } from '../types/user';

// Base URL for the API
const API_BASE_URL = 'http://localhost:5000/api/users';

// Function to fetch all users with pagination
export const fetchUsers = async (pageNumber: number = 1, pageSize: number = 10): Promise<PaginatedResult<User>> => {
    try {
        const response = await axios.get(`${API_BASE_URL}?pageNumber=${pageNumber}&pageSize=${pageSize}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
        throw error;
    }
};

// Function to fetch a single user by ID
export const fetchUserById = async (id: string): Promise<User> => {
    try {
        const response = await axios.get(`${API_BASE_URL}/${id}`);
        return response.data;
    } catch (error) {
        console.error(`Error fetching user with ID ${id}:`, error);
        throw error;
    }
};

// Function to search users by a query term
export const searchUsers = async (searchTerm: string): Promise<User[]> => {
    try {
        const response = await axios.get(`${API_BASE_URL}/search?query=${searchTerm}`);
        return response.data;
    } catch (error) {
        console.error('Error searching users:', error);
        throw error;
    }
};