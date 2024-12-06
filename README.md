# -Identifying-Reversed-Diabetes-Patients-Using-ML-Algorithms-with-Diet-Recommendation-
The uploaded document is a project report titled “Identifying Reversed Diabetes Patients Using ML Algorithms with Diet Recommendation”. It details the development of a real-time medical application using machine learning algorithms like KNN, Random Forest, and Naive Bayes. The goal is to predict diabetes and reverse diabetes cases while recommending dietary plans. The system is designed for hospital use and incorporates supervised learning algorithms.

Identifying Reversed Diabetes Patients Using ML Algorithms with Diet Recommendation
Project Overview
This project presents a real-time medical application designed to predict diabetes and identify reversed diabetes patients. It leverages machine learning algorithms and provides dietary recommendations to aid healthcare professionals. The system is tailored for hospital environments, allowing physicians and patients to interact seamlessly via a web-based GUI.

Features
Diabetes Prediction: Early detection using supervised machine learning models.
Reverse Diabetes Prediction: Identifies patients with the potential for diabetes reversal.
Dietary Recommendations: Provides personalized diet plans based on predictions.
User Roles: Supports multiple users, including admin, doctors, receptionists, and patients.
Algorithms Used
K-Nearest Neighbors (KNN): Achieved an accuracy of ~97.18%.
Random Forest: Accuracy of ~91.18%.
Naive Bayes: Efficient and versatile for medical datasets.
Technologies
Frontend: HTML, CSS, JavaScript, jQuery.
Backend: C# (ASP.NET Framework).
Database: SQL Server.
IDE: Visual Studio.
Machine Learning Library: Custom implementations of KNN and Naive Bayes.
System Architecture
The project follows a three-tier architecture:

Presentation Layer: User interface for data input and result visualization.
Business Logic Layer: Implements machine learning algorithms for predictions.
Data Layer: Stores and retrieves data using SQL Server.
Installation
Clone the repository:
bash
Copy code
Open the solution in Visual Studio.
Configure the SQL Server connection in the web.config file.
Run the application using IIS Express or your preferred hosting server.
Dataset
Source: Kaggle Diabetes Dataset.
Attributes Used:
Pregnancies
Glucose Levels
Blood Pressure
Skin Thickness
Insulin
BMI
Diabetes Pedigree Function
Age
Outcome
Results
KNN Algorithm: Accuracy - 97.18%, Time - 1.6s.
Random Forest Algorithm: Accuracy - 91.18%, Time - 2.6s.
Future Scope
Extend to integrate EHR and wearable devices.
Enhance accuracy with additional datasets.
Support more advanced dietary recommendations using AI.
