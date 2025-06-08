'use client';

import React, { useEffect, useState } from 'react';
import UserList from '../components/UserList';
import LoadingSpinner from '../components/LoadingSpinner';
import SearchBar from '../components/SearchBar';
import Pagination from '../components/Pagination';
import { fetchUsers, searchUsers } from '../services/userService';
import { User, PaginatedResult } from '../types/user';

export default function Home() {
    const [users, setUsers] = useState<PaginatedResult<User> | null>(null);
    const [searchResults, setSearchResults] = useState<User[] | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [searchTerm, setSearchTerm] = useState('');
    const pageSize = 10;

    // Fetch users from the backend
    const getUsers = async (page: number) => {
        try {
            setLoading(true);
            const userData = await fetchUsers(page, pageSize);
            setUsers(userData);
            setSearchResults(null); // Clear search results when fetching all users
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    // Handle search
    const handleSearch = async (term: string) => {
        if (!term.trim()) {
            setSearchTerm('');
            setSearchResults(null);
            await getUsers(1);
            setCurrentPage(1);
            return;
        }

        try {
            setLoading(true);
            setSearchTerm(term);
            const results = await searchUsers(term);
            setSearchResults(results);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    // Handle page change
    const handlePageChange = async (page: number) => {
        setCurrentPage(page);
        await getUsers(page);
    };

    // Fetch users when component mounts
    useEffect(() => {
        getUsers(1);
    }, []);

    if (loading) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <LoadingSpinner />
            </div>
        );
    }

    if (error) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="bg-red-100 border border-red-400 text-red-700 px-4 py-3 rounded">
                    <strong className="font-bold">Error: </strong>
                    <span className="block sm:inline">{error}</span>
                </div>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50">
            <div className="container mx-auto px-4 py-8">
                <div className="text-center mb-8">
                    <h1 className="text-4xl font-bold text-gray-900 mb-2">User Management</h1>
                    <p className="text-gray-600">Browse and search through our user directory</p>
                </div>

                {/* Search Bar */}
                <div className="mb-8 flex justify-center">
                    <div className="w-full max-w-md">
                        <SearchBar onSearch={handleSearch} />
                    </div>
                </div>

                {/* Results Info */}
                <div className="mb-6">
                    {searchTerm ? (
                        <div className="text-center">
                            <p className="text-gray-600">
                                Search results for "{searchTerm}": {searchResults?.length || 0} users found
                            </p>
                            <button
                                onClick={() => handleSearch('')}
                                className="text-blue-600 hover:text-blue-800 underline ml-2"
                            >
                                Clear search
                            </button>
                        </div>
                    ) : users ? (
                        <p className="text-center text-gray-600">
                            Showing {users.items.length} of {users.totalCount} users
                        </p>
                    ) : null}
                </div>

                {/* User List */}
                <UserList users={searchResults || users?.items || []} />

                {/* Pagination - only show when not searching */}
                {!searchTerm && users && users.totalPages > 1 && (
                    <div className="mt-8 flex justify-center">
                        <Pagination
                            currentPage={currentPage}
                            totalPages={users.totalPages}
                            onPageChange={handlePageChange}
                        />
                    </div>
                )}
            </div>
        </div>
    );
}