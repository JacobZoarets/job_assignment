// next.config.js
// This file contains configuration settings for the Next.js application.

const nextConfig = {
    // Enabling React Strict Mode for highlighting potential problems in the application
    reactStrictMode: true,
    
    // Configuring the base path for the application if needed
    // basePath: '/your-base-path',

    // Enabling image optimization for better performance
    images: {
        domains: ['randomuser.me'], // Allowing images from the Random User Generator API
    },

    // Custom Webpack configuration if needed
    webpack: (config) => {
        // Example: Adding a custom loader or plugin
        // config.module.rules.push({
        //     test: /\.svg$/,
        //     use: ['@svgr/webpack'],
        // });

        return config;
    },
};

module.exports = nextConfig;