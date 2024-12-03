
# File Upload API

This repository provides a utility for file uploads using ASP.NET WEB API. The API supports various file formats and validates file metadata such as size, type, and upload success. The repository includes detailed test cases for valid uploads, invalid formats, and oversized files.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [API Endpoints](#api-endpoints)
  - [1. Valid File and Format](#1-valid-file-and-format)
  - [2. Invalid File Format or Missing File](#2-invalid-file-format-or-missing-file)
  - [3. Large File Upload](#3-large-file-upload)
- [Response Format](#response-format)
- [Testing](#testing)
- [Technologies Used](#technologies-used)
- [How to Run Locally](#how-to-run-locally)

---

## Overview

This project demonstrates a file upload feature through a RESTful API. It supports:
- Validation of file format (`JPEG`, `PNG`, `PDF`).
- Restriction on file size (maximum `5 MB`).
- Testing scripts to ensure API functionality and reliability.

## Features
- **File Validation**: Supports only specific formats and ensures file size does not exceed `5 MB`.
- **Metadata Response**: Returns uploaded file details such as file name, type, size, and path.
- **Detailed Error Handling**: Handles invalid formats, missing files, and large file uploads gracefully.

---

## API Endpoints

### 1. Valid File and Format
- **URL**: `http://localhost:61123/api/files/upload`
- **Method**: `POST`
- **Headers**: 
  - `Content-Type`: `multipart/form-data`
- **Request Body**:
  - `file`: The file to be uploaded.
- **Success Response**:
  ```json
  {
    "status": "success",
    "message": "File uploaded successfully",
    "data": {
      "fileName": "example.png",
      "filePath": "/Uploads/example.png",
      "fileSize": "112 KB",
      "fileType": "image/png"
    }
  }
  ```

---

### 2. Invalid File Format or Missing File
- **URL**: `http://localhost:61123/api/files/upload`
- **Method**: `POST`
- **Headers**: 
  - `Content-Type`: `multipart/form-data`
- **Request Body**:
  - `file`: No file or unsupported format.
- **Failure Response**:
  ```json
  {
    "status": "error",
    "message": "Invalid file format or no file provided."
  }
  ```

---

### 3. Large File Upload
- **URL**: `http://localhost:61123/api/files/upload`
- **Method**: `POST`
- **Headers**: 
  - `Content-Type`: `multipart/form-data`
- **Request Body**:
  - `file`: File exceeding `5 MB`.
- **Failure Response**:
  ```json
  {
    "status": "error",
    "message": "File size exceeds the 5MB limit."
  }
  ```

---

## Response Format

All responses are returned in JSON format with the following structure:

- **Success**:
  ```json
  {
    "status": "success",
    "message": "<success_message>",
    "data": {
      "fileName": "<file_name>",
      "filePath": "<file_path>",
      "fileSize": "<file_size>",
      "fileType": "<file_type>"
    }
  }
  ```
- **Failure**:
  ```json
  {
    "status": "error",
    "message": "<error_message>"
  }
  ```

---

## Testing

The Postman collection includes tests for:
1. Valid file uploads.
2. Invalid file formats or missing files.
3. Large file uploads.

To use:
- Import the `File Upload` Postman collection JSON file.
- Run individual requests or the full collection.

---

## Technologies Used

- **Backend**: ASP.NET Core
- **Validation**: Custom Middleware for file type and size validation
- **Testing**: Postman tests and scripts
- **File Storage**: Local file system

---

## How to Run Locally

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/file-upload-api.git
   cd file-upload-api
   ```

2. Open the project in Visual Studio.

3. Configure the local environment:
   - Update the `appsettings.json` file for file storage settings if necessary.

4. Run the project:
   - Start the API using Visual Studio.

5. Test with Postman:
   - Import the provided Postman collection and execute requests.

---
## Postman Images
  - Valid File Upload
![Valid File Upload 1](https://github.com/user-attachments/assets/6617a3db-994f-409b-af02-9e31453d81ee)
![Valid File Upload 2](https://github.com/user-attachments/assets/3796e245-a0ee-49ab-813f-b0b1753d6f45)

  - Invalid file upload / No file upload
![Invalid file input or No input 1](https://github.com/user-attachments/assets/eb4bc52b-3b2a-463d-a52d-aea6ab1bf61d)
![Invalid file input or No input 2](https://github.com/user-attachments/assets/4c306a17-6030-4f2b-b89c-24804f9ac0ba)

  - Large file upload
![Large file upload 1](https://github.com/user-attachments/assets/cbf30387-16a5-4718-908d-c66c48f954d7)
![Large file upload 2](https://github.com/user-attachments/assets/8a205322-cc52-4012-a23e-6fe4f314f577)





