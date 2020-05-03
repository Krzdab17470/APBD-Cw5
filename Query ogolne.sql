USE S17470
select MAX(IdEnrollment) AS MAXID FROM Enrollment WHERE Semester = 1 AND IdStudy = 10
select  * FROM STUDIES
INSERT INTO STUDIES VALUES (14,'IT')
SELECT IdStudy FROM Studies WHERE name='IT'
SELECT * FROM ENROLLMENT
Select IdEnrollment FROM Enrollment WHERE IdEnrollment = (Select MAX(IdEnrollment) FROM Enrollment)
Select IdEnrollment FROM Enrollment WHERE IdEnrollment = (Select MAX(IdEnrollment) FROM Enrollment)
SELECT * FROM STUDENT
INSERT INTO ENROLLMENT (IDENROLLMENT,SEMESTER,IDSTUDY,STARTDATE) VALUES (15,1,10, CONVERT(date,'2019-08-05',0))
Select IdEnrollment FROM Enrollment WHERE IdEnrollment = (Select MAX(IdEnrollment) FROM Enrollment)
--TRUNCATE TABLE ENROLLMENT
UPDATE STUDENT SET IDENROLLMENT = 1 WHERE IDENROLLMENT = 11;
SELECT MAX(IDENROLLMENT) FROM ENROLLMENT
EXEC PROMOTESTUDENTS @STUDIES = 'Logistics', @SEMESTER = 3;
--DELETE FROM Enrollment WHERE IDENROLLMENT = 10;

insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (1, 1, 10, '1/21/2020');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (2, 2, 10, '5/22/2019');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (3, 3, 10, '2/24/2020');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (4, 1, 12, '12/27/2019');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (5, 2, 12, '2/6/2020');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (6, 3, 12, '10/22/2019');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (7, 1, 14, '10/2/2019');
insert into ENROLLMENT (IdEnrollment, Semester, IdStudy, StartDate) values (8, 2, 14, '12/22/2019');


insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (1, 'Elisha', 'Shakshaft', '4/19/2020', 1);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (2, 'Frasquito', 'Saturley', '1/7/2020', 1);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (3, 'Georgie', 'Coche', '2/4/2020', 3);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (4, 'Steward', 'Valek', '3/3/2020', 3);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (5, 'Gabbie', 'Niess', '12/25/2019', 3);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (6, 'Joshia', 'Fetters', '1/30/2020', 4);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (7, 'Donn', 'Carnow', '2/19/2020', 4);
insert into STUDENT (IndexNumber, FirstName, LastName, BirthDate, IdEnrollment) values (8, 'Meier', 'O''Dunneen', '8/17/2019', 4);