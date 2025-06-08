import React from 'react';

// LoadingSpinner component to display a loading indicator while data is being fetched
const LoadingSpinner: React.FC = () => {
    return (
        <div className="flex justify-center items-center h-screen">
            {/* Spinner element */}
            <div className="animate-spin rounded-full h-32 w-32 border-t-4 border-blue-500"></div>
        </div>
    );
};

export default LoadingSpinner;