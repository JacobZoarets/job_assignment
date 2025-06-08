import React from 'react';

interface PaginationProps {
    currentPage: number;
    totalPages: number;
    onPageChange: (page: number) => void;
}

const Pagination: React.FC<PaginationProps> = ({ currentPage, totalPages, onPageChange }) => {
    const handlePageChange = (page: number) => {
        if (page >= 1 && page <= totalPages) {
            onPageChange(page);
        }
    };

    // Generate array of page numbers to show
    const generatePageNumbers = () => {
        const delta = 2; // Number of pages to show on each side of current page
        const range = [];
        const rangeWithDots = [];

        for (let i = Math.max(2, currentPage - delta); i <= Math.min(totalPages - 1, currentPage + delta); i++) {
            range.push(i);
        }

        if (currentPage - delta > 2) {
            rangeWithDots.push(1, '...');
        } else {
            rangeWithDots.push(1);
        }

        rangeWithDots.push(...range);

        if (currentPage + delta < totalPages - 1) {
            rangeWithDots.push('...', totalPages);
        } else if (totalPages > 1) {
            rangeWithDots.push(totalPages);
        }

        return rangeWithDots;
    };

    if (totalPages <= 1) return null;

    return (
        <div className="flex items-center justify-center space-x-1">
            {/* Previous button */}
            <button
                onClick={() => handlePageChange(currentPage - 1)}
                disabled={currentPage === 1}
                className={`px-3 py-2 rounded-md text-sm font-medium ${
                    currentPage === 1
                        ? 'text-gray-400 cursor-not-allowed bg-gray-100'
                        : 'text-gray-700 bg-white border border-gray-300 hover:bg-gray-50'
                }`}
            >
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 19l-7-7 7-7" />
                </svg>
            </button>

            {/* Page numbers */}
            {generatePageNumbers().map((pageNumber, index) => (
                <button
                    key={index}
                    onClick={() => typeof pageNumber === 'number' ? handlePageChange(pageNumber) : undefined}
                    disabled={pageNumber === '...'}
                    className={`px-3 py-2 rounded-md text-sm font-medium ${
                        pageNumber === currentPage
                            ? 'bg-blue-600 text-white'
                            : pageNumber === '...'
                            ? 'text-gray-400 cursor-default'
                            : 'text-gray-700 bg-white border border-gray-300 hover:bg-gray-50'
                    }`}
                >
                    {pageNumber}
                </button>
            ))}

            {/* Next button */}
            <button
                onClick={() => handlePageChange(currentPage + 1)}
                disabled={currentPage === totalPages}
                className={`px-3 py-2 rounded-md text-sm font-medium ${
                    currentPage === totalPages
                        ? 'text-gray-400 cursor-not-allowed bg-gray-100'
                        : 'text-gray-700 bg-white border border-gray-300 hover:bg-gray-50'
                }`}
            >
                <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                </svg>
            </button>
        </div>
    );
};

export default Pagination;