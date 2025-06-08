// This file defines TypeScript interfaces for user data, ensuring type safety throughout the application.

export interface User {
    id: string; // Unique identifier for the user
    firstName: string; // User's first name
    lastName: string; // User's last name
    email: string; // User's email address
    dateOfBirth: string; // User's date of birth in ISO format
    phone: string; // User's phone number
    address: string; // User's address
    profilePicture: string; // URL of the user's profile picture
}

export interface PaginatedResult<T> {
    items: T[];
    totalCount: number;
    pageSize: number;
    currentPage: number;
    totalPages: number;
}