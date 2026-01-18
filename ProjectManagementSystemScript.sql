use  projectdatabase2;
SET SQL_SAFE_UPDATES = 0;
UPDATE features  
SET FeatureName = "Assign Task To Team"  
WHERE Id = 6;
SET SQL_SAFE_UPDATES = 1;
select * from features;
select *from projects;
update projects
set Status = 3
where Id = 1;


select * from tasks;
DELETE FROM tasks
WHERE id > 38;

delete from imports where id> 0;

Insert Into comments(UserName,TaskId,CommentedAt,CommentMessage,UserId)
values ('Rajibul', 5, NOW(), 'payment gateway is now ready for final testing',1 ); 
   --  ('Rajibul', 8, NOW(), 'UI changes look great! Just a few minor adjustments needed.', 1),  
--     ('Rajibul', 9, NOW(), 'Cloud setup is in progress. Expecting completion by next week.', 1),  
--     ('Rajibul', 10, NOW(), 'Database optimization is yielding good results. Further tests required.', 1),  
--     ('RAjibul', 11, NOW(), 'Security audit identified some vulnerabilities. Fixes are underway.', 1);  

INSERT INTO Projects(ProjectName, StartDate, EndDate, Status) VALUES
('Eclipse AI Assistant', '2024-07-01', '2024-10-05', 1),
('Aurora CRM Reboot', '2024-08-20', '2024-11-28', 1),
('Helix E-Commerce Hub', '2024-09-10', '2024-12-20', 2),
('Lyra Learning Portal', '2024-10-01', '2024-12-10', 1),
('Vortex Analytics Core', '2024-03-15', '2024-06-30', 3),
('Neon Social Engine', '2024-01-20', '2024-05-05', 3),
('Stratos Task Manager', '2024-02-25', '2024-07-15', 3),
('Nova Payment Gateway', '2024-04-01', '2024-09-01', 2),
('Quasar Booking System', '2024-05-10', '2024-10-10', 1),
('Aether Backup Manager', '2024-06-05', '2024-08-20', 2),
('Titan HR Platform', '2024-07-15', '2024-10-30', 1),
('Blaze Performance Tracker', '2024-08-01', '2024-11-15', 1),
('Echo Remote Workspace', '2024-09-12', '2024-12-18', 2),
('Vertex DevOps Dashboard', '2024-10-05', '2025-01-10', 2);


INSERT INTO Tasks (taskName,taskDescription, projectId, status, dueDate) VALUES
('Manish Testing','No Description', 6, 2, '2024-11-30');
('Cloud Infrastructure Setup', 7, 1, '2024-09-10'),
('Database Optimization', 8, 2, '2025-02-15'),
('Security Assessment', 12, 2, '2025-06-10'),
('Marketing Strategy Planning', 10, 2, '2025-04-20'),
('User Beta Testing', 11, 3, '2025-03-01'),
('Smart Contracts Development', 14, 2, '2025-07-15'),
('Sensor Integration', 15, 3, '2024-09-30'),
('Data Encryption Module', 12, 2, '2025-05-10'),
('Performance Optimization', 5, 2, '2025-04-25'),
('Mobile App Development', 6, 2, '2024-12-15'),
('AI Model Fine-Tuning', 5, 2, '2025-05-10'),
('Load Testing', 3, 3, '2024-11-30'),
('Customer Onboarding Features', 9, 1, '2025-02-20'),
('Blockchain Node Deployment', 14, 2, '2025-09-10');

select * from featuresCategory;
select * from features;
select *from teammembers;
INSERT INTO teammembers (Name, Role) VALUES
('Vansh Sikka','UI/UX Designer'),
('Aarhaam Jain','Research Analyst'),
('Ekansh Mahajan','Automation'),
('Sanket Sharma','Risk Analysis'),
('Subhash Kumar','Digital Marketing');
select * from projects;
INSERT INTO Features (CategoryId, FeatureName, PathUrl) VALUES
(1, 'Display Project Status', 'project/status'),
(1, 'Update Project Status', 'project/update-status'),
(1, 'Display Project Report', 'project/reports'),
(2, 'Display Task Status', 'task/status'),
(2, 'Update Task Status', 'task/update-status'),
(2, 'Assign Task', 'task'),
(3, 'Display Team Members', 'team/task-team'),
(3, 'Display Task Team', 'team/task-team');

INSERT INTO FeaturesCategory(Id, Category, icon) Values 
(1,'Project','pi pi-server'),
(2,'Task','pi pi pi-list'),
(3,'Team','pi pi-users');

Delete from features where Id = 10;
Drop table Features;
Drop table  featuresCategory;
Drop table Comments;

UPDATE tasks
SET taskDescription = CASE 
    WHEN id = 5 THEN 'Integrate a secure and reliable payment gateway to facilitate transactions.'
    WHEN id = 8 THEN 'Enhance the UI/UX of the application for better user experience and engagement.'
    WHEN id = 9 THEN 'Set up a scalable and secure cloud infrastructure for hosting applications.'
    WHEN id = 10 THEN 'Optimize the database performance by indexing, query tuning, and normalization.'
    WHEN id = 11 THEN 'Conduct a thorough security assessment to identify vulnerabilities and risks.'
    WHEN id = 12 THEN 'Plan and strategize marketing initiatives to increase brand awareness and customer acquisition.'
    WHEN id = 13 THEN 'Conduct beta testing with real users to gather feedback and identify bugs.'
    WHEN id = 18 THEN 'Develop and enhance the mobile application for improved functionality and performance.'
    WHEN id = 20 THEN 'Perform load testing to ensure the system can handle expected user traffic and stress conditions.'
    WHEN id = 21 THEN 'Implement customer onboarding features to simplify and enhance user registration and first-time experience.'
    WHEN id = 23 THEN 'Further improve the UI/UX with refined designs and user-friendly interfaces.'
    WHEN id = 24 THEN 'Reinforce cloud infrastructure for better performance, reliability, and scalability.'
    WHEN id = 25 THEN 'Apply database optimization techniques to improve speed and efficiency.'
    WHEN id = 26 THEN 'Perform security audits and implement best practices to safeguard application data.'
    WHEN id = 27 THEN 'Devise a comprehensive marketing strategy to boost product reach and engagement.'
    WHEN id = 28 THEN 'Facilitate beta testing with a focus on usability, functionality, and performance analysis.'
    WHEN id = 33 THEN 'Enhance mobile application development with additional features and bug fixes.'
    WHEN id = 35 THEN 'Execute load testing scenarios to evaluate system behavior under varying loads.'
    ELSE taskDescription
END
WHERE id IN (5, 8, 9, 10, 11, 12, 13, 18, 20, 21, 23, 24, 25, 26, 27, 28, 33, 35);

select * from users;

select * from comments;
select * from taskAssignments;

use projectDatabase2;

select * from tasks;
select * from __efmigrationshistory;

create database jumpserver;