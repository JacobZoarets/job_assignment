import React, { useEffect, useState } from 'react';
import UserList from '../../components/UserList';
import Pagination from '../../components/Pagination';
import SearchBar from '../../components/SearchBar';
import LoadingSpinner from '../../components/LoadingSpinner';
import { fetchUsers } from '../../services/userService';

const UsersPage = () => {
    const [users, setUsers] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(0);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        const loadUsers = async () => {
            setLoading(true);
            setError(null);
            try {
                const response = await fetchUsers(currentPage, searchTerm);
                setUsers(response.data);
                setTotalPages(response.totalPages);
            } catch (err) {
                setError('Failed to fetch users. Please try again later.');
            } finally {
                setLoading(false);
            }
        };

        loadUsers();
    }, [currentPage, searchTerm]);

    const handlePageChange = (page) => {
        setCurrentPage(page);
    };

    const handleSearch = (term) => {
        setSearchTerm(term);
        setCurrentPage(1); // Reset to first page on search
    };

    return (
        <div className="users-page">
            <h1>User Management</h1>
            <SearchBar onSearch={handleSearch} />
            {loading && <LoadingSpinner />}
            {error && <div className="error">{error}</div>}
            {!loading && !error && (
                <>
                    <UserList users={users} />
                    <Pagination 
                        currentPage={currentPage} 
                        totalPages={totalPages} 
                        onPageChange={handlePageChange} 
                    />
                </>
            )}
        </div>
    );
};

export default UsersPage;