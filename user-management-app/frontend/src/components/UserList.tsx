import React from 'react';
import Link from 'next/link';
import { User } from '../types/user';

interface UserListProps {
    users: User[];
}

const UserList: React.FC<UserListProps> = ({ users }) => {
    if (users.length === 0) {
        return (
            <div className="text-center py-12">
                <div className="text-gray-500 text-lg">No users found</div>
                <p className="text-gray-400 mt-2">Try adjusting your search criteria</p>
            </div>
        );
    }

    return (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
            {users.map((user) => (
                <Link key={user.id} href={`/users/${user.id}`}>
                    <div className="bg-white rounded-lg shadow-md hover:shadow-lg transition-shadow duration-300 p-6 cursor-pointer border border-gray-200 hover:border-blue-300">
                        <div className="flex flex-col items-center">
                            <div className="w-16 h-16 bg-gradient-to-r from-blue-500 to-purple-600 rounded-full flex items-center justify-center mb-4">
                                {user.profilePicture ? (
                                    <img
                                        src={user.profilePicture}
                                        alt={`${user.firstName} ${user.lastName}`}
                                        className="w-16 h-16 rounded-full object-cover"
                                    />
                                ) : (
                                    <span className="text-white font-semibold text-lg">
                                        {user.firstName[0]}{user.lastName[0]}
                                    </span>
                                )}
                            </div>
                            
                            <h3 className="text-lg font-semibold text-gray-900 mb-1 text-center">
                                {user.firstName} {user.lastName}
                            </h3>
                            
                            <p className="text-gray-600 text-sm mb-2 text-center">{user.email}</p>
                            
                            <div className="text-gray-500 text-xs space-y-1 text-center">
                                <p>{user.phone}</p>
                                <p className="truncate w-full">{user.address}</p>
                            </div>
                        </div>
                    </div>
                </Link>
            ))}
        </div>
    );
};

export default UserList;