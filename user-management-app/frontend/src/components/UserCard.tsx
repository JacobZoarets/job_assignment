import React from 'react';

// Define the props for the UserCard component
interface UserCardProps {
    id: string; // Unique identifier for the user
    firstName: string; // User's first name
    lastName: string; // User's last name
    email: string; // User's email address
    profilePicture: string; // URL for the user's profile picture
    onClick: (id: string) => void; // Function to handle click events
}

// UserCard component definition
const UserCard: React.FC<UserCardProps> = ({ id, firstName, lastName, email, profilePicture, onClick }) => {
    return (
        <div 
            className="user-card" 
            onClick={() => onClick(id)} // Call the onClick function with the user's ID when the card is clicked
            style={{ cursor: 'pointer', border: '1px solid #ccc', borderRadius: '8px', padding: '16px', margin: '8px' }}
        >
            <img 
                src={profilePicture} 
                alt={`${firstName} ${lastName}`} 
                className="profile-picture" 
                style={{ width: '100px', height: '100px', borderRadius: '50%' }} // Style for the profile picture
            />
            <h3>{`${firstName} ${lastName}`}</h3> {/* Display user's full name */}
            <p>{email}</p> {/* Display user's email */}
        </div>
    );
};

export default UserCard; // Export the UserCard component for use in other parts of the application