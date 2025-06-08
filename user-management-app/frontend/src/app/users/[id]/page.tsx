'use client';

import { useEffect, useState } from 'react';
import { useParams, useRouter } from 'next/navigation';
import UserDetail from '../../../components/UserDetail';
import LoadingSpinner from '../../../components/LoadingSpinner';
import { fetchUserById } from '../../../services/userService';
import { User } from '../../../types/user';

export default function UserPage() {
    const params = useParams();
    const router = useRouter();
    const id = params.id as string;
    
    const [user, setUser] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (id) {
            const getUser = async () => {
                try {
                    setLoading(true);
                    const fetchedUser = await fetchUserById(id);
                    setUser(fetchedUser);
                } catch (err) {
                    setError('Failed to fetch user data.');
                    console.error('Error fetching user:', err);
                } finally {
                    setLoading(false);
                }
            };

            getUser();
        }
    }, [id]);

    if (loading) {
        return <LoadingSpinner />;
    }

    if (error) {
        return (
            <div className="min-h-screen bg-gray-50 flex items-center justify-center">
                <div className="max-w-md w-full bg-white rounded-lg shadow-lg p-6">
                    <div className="text-center">
                        <div className="text-red-500 text-xl mb-4">❌</div>
                        <h2 className="text-xl font-semibold text-gray-900 mb-2">Error</h2>
                        <p className="text-gray-600 mb-4">{error}</p>
                        <button
                            onClick={() => router.push('/')}
                            className="bg-blue-500 text-white px-4 py-2 rounded-lg hover:bg-blue-600 transition-colors"
                        >
                            Back to Home
                        </button>
                    </div>
                </div>
            </div>
        );
    }

    return user ? <UserDetail user={user} /> : null;
}