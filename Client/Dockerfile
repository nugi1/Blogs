# Use Node.js image
FROM node:18 AS build

# Set working directory
WORKDIR /app

# Copy package files & install dependencies
COPY package*.json ./
RUN npm install

# Copy the app files
COPY . .

# Expose Angular default port
EXPOSE 4200

# Run Angular dev server
CMD ["npm", "run", "start", "--", "--host", "0.0.0.0"]

