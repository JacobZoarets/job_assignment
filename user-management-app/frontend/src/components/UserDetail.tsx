'use client';

import React from 'react';
import { useRouter } from 'next/navigation';
import { User } from '../types/user';

interface UserDetailProps {
    user: User;
}

export default function UserDetail({ user }: UserDetailProps) {
    const router = useRouter();

    return (
        <div className="min-h-screen bg-gray-50 py-8">
            <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8">
                {/* Header with back button */}
                <div className="mb-8">
                    <button
                        onClick={() => router.push('/')}
                        className="inline-flex items-center text-sm font-medium text-gray-500 hover:text-gray-700 transition-colors"
                    >
                        <svg className="mr-2 h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 19l-7-7 7-7" />
                        </svg>
                        Back to Users
                    </button>
                </div>

                {/* User Details Card */}
                <div className="bg-white rounded-lg shadow-lg overflow-hidden">
                    <div className="p-8">
                        <div className="flex flex-col md:flex-row md:items-start gap-8">
                            {/* Profile Picture */}
                            <div className="flex-shrink-0">
                                <img
                                    src={user.profilePicture}
                                    alt={`${user.firstName} ${user.lastName}`}
                                    className="h-48 w-48 rounded-full object-cover border-4 border-gray-200"
                                />
                            </div>

                            {/* User Information */}
                            <div className="flex-1">
                                {/* Name */}
                                <h1 className="text-4xl font-bold text-gray-900 mb-6">
                                    {user.firstName} {user.lastName}
                                </h1>

                                {/* Contact Information */}
                                <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
                                    <div className="space-y-4">
                                        <div>
                                            <h3 className="text-sm font-medium text-gray-500 uppercase tracking-wide">Email</h3>
                                            <p className="mt-1 text-lg text-gray-900">{user.email}</p>
                                        </div>
                                        
                                        <div>
                                            <h3 className="text-sm font-medium text-gray-500 uppercase tracking-wide">Phone</h3>
                                            <p className="mt-1 text-lg text-gray-900">{user.phone}</p>
                                        </div>
                                        
                                        <div>
                                            <h3 className="text-sm font-medium text-gray-500 uppercase tracking-wide">Date of Birth</h3>
                                            <p className="mt-1 text-lg text-gray-900">
                                                {new Date(user.dateOfBirth).toLocaleDateString('en-US', {
                                                    year: 'numeric',
                                                    month: 'long',
                                                    day: 'numeric'
                                                })}
                                            </p>
                                        </div>
                                    </div>

                                    <div className="space-y-4">
                                        <div>
                                            <h3 className="text-sm font-medium text-gray-500 uppercase tracking-wide">Address</h3>
                                            <p className="mt-1 text-lg text-gray-900 leading-relaxed">{user.address}</p>
                                        </div>
                                        
                                        {user.city && (
                                            <div>
                                                <h3 className="text-sm font-medium text-gray-500 uppercase tracking-wide">City</h3>
                                                <p className="mt-1 text-lg text-gray-900">{user.city}</p>
                                            </div>
                                        )}
                                        
                                        {user.country && (
                                            <div>
                                                <h3 className="text-sm font-medium text-gray-500 uppercase tracking-wide">Country</h3>
                                                <p className="mt-1 text-lg text-gray-900">{user.country}</p>
                                            </div>
                                        )}
                                    </div>
                                </div>

                                {/* Action Buttons */}
                                <div className="mt-8 pt-6 border-t border-gray-200">
                                    <div className="flex flex-col sm:flex-row gap-4">
                                        <button
                                            onClick={() => router.push('/')}
                                            className="bg-blue-600 text-white px-6 py-3 rounded-lg font-medium hover:bg-blue-700 transition-colors focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2"
                                        >
                                            Back to All Users
                                        </button>
                                        
                                        <a
                                            href={`mailto:${user.email}`}
                                            className="bg-gray-100 text-gray-700 px-6 py-3 rounded-lg font-medium hover:bg-gray-200 transition-colors focus:outline-none focus:ring-2 focus:ring-gray-500 focus:ring-offset-2 text-center"
                                        >
                                            Send Email
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}