# Use Node.js image to build the Angular app
FROM node:18 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the package.json and package-lock.json files
COPY package*.json ./

# Install the dependencies
RUN npm install

# Copy all the other files
COPY . .

# Build the Angular application
RUN npm run build --prod

# Use Nginx to serve the built Angular app
FROM nginx:alpine

# Copy the build output to the Nginx directory
COPY --from=build /app/dist/client /usr/share/nginx/html

# Expose port 80
EXPOSE 80

# Start Nginx to serve the app
CMD ["nginx", "-g", "daemon off;"]
