# SpringCTAssignment

1.	I was unable to connect to my localhost SQLServer due to permission issues as I do not have admin access. 
    a.	Hence I have created entities and migration files as well in the code base but have not added them to the db.
2.	You can use the following script to add default courses in db:
    a.	Insert into Courses values (‘Physics’, ‘2025-29-03 15:30:00.00000’, 0)
    b.	Insert into Courses values (‘Mathematics, ‘2025-29-03 15:30:00.00000’, 0)
    c.	Insert into Courses values (‘Chemistry, ‘2025-29-03 15:30:00.00000’, 0)
    d.	Insert into Courses values (‘Biology, ‘2025-29-03 15:30:00.00000’, 0)
    e.	Insert into Courses values (‘Economics, ‘2025-29-03 15:30:00.00000’, 0)
    f.	Insert into Courses values (‘Psychology, ‘2025-29-03 15:30:00.00000’, 0)
3.	You can use the GetCourses Api end point to fetch all course data to link with students
4.	Database schema are available in Migration file
5.	Authentication part is still left
    a. I tried to implement basic auth
    a. I had used Session but it did not work (it should not either). Need to update
