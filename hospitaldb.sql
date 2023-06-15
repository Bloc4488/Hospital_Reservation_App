-- phpMyAdmin SQL Dump
-- version 5.1.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Jun 15, 2023 at 02:30 PM
-- Server version: 5.7.24
-- PHP Version: 8.0.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `hospitaldb`
--
CREATE DATABASE IF NOT EXISTS `hospitaldb` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `hospitaldb`;

-- --------------------------------------------------------

--
-- Table structure for table `doctors`
--

DROP TABLE IF EXISTS `doctors`;
CREATE TABLE `doctors` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `speciality_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `doctors`
--

INSERT INTO `doctors` (`id`, `user_id`, `speciality_id`) VALUES
(5, 28, 4),
(7, 29, 0);

-- --------------------------------------------------------

--
-- Table structure for table `doctor_notes`
--

DROP TABLE IF EXISTS `doctor_notes`;
CREATE TABLE `doctor_notes` (
  `doctor_note_id` int(11) NOT NULL,
  `reservation_id` int(11) NOT NULL,
  `note` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `doctor_notes`
--

INSERT INTO `doctor_notes` (`doctor_note_id`, `reservation_id`, `note`) VALUES
(1, 19, '1212123132165446'),
(2, 22, '1321231'),
(3, 23, 'Zmień nogę!'),
(4, 23, '132132'),
(5, 21, 'Zmień nogę!');

-- --------------------------------------------------------

--
-- Table structure for table `grades_and_comments`
--

DROP TABLE IF EXISTS `grades_and_comments`;
CREATE TABLE `grades_and_comments` (
  `comment_id` int(11) NOT NULL,
  `reservation_id` int(11) NOT NULL,
  `grade` int(11) DEFAULT NULL,
  `comment` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `grades_and_comments`
--

INSERT INTO `grades_and_comments` (`comment_id`, `reservation_id`, `grade`, `comment`) VALUES
(1, 19, NULL, 'Hello'),
(2, 19, 4, '123123'),
(4, 17, 4, '1212'),
(5, 17, 5, 'DADADAD'),
(6, 19, 4, '12'),
(8, 21, NULL, 'Dzięki za notatkę!');

-- --------------------------------------------------------

--
-- Table structure for table `reservations`
--

DROP TABLE IF EXISTS `reservations`;
CREATE TABLE `reservations` (
  `reservation_id` int(11) NOT NULL,
  `patient_id` int(11) NOT NULL,
  `doctor_id` int(11) NOT NULL,
  `date_res` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `reservations`
--

INSERT INTO `reservations` (`reservation_id`, `patient_id`, `doctor_id`, `date_res`) VALUES
(17, 28, 23, '2023-06-13 10:00:00'),
(18, 29, 23, '2023-06-13 11:00:00'),
(19, 28, 23, '2023-06-15 10:00:00'),
(21, 26, 29, '2023-06-15 09:00:00'),
(22, 26, 28, '2023-06-15 11:00:00'),
(23, 28, 29, '2023-06-23 13:00:00'),
(24, 28, 29, '2023-06-16 11:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `specialties`
--

DROP TABLE IF EXISTS `specialties`;
CREATE TABLE `specialties` (
  `speciality_id` int(11) NOT NULL,
  `name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `specialties`
--

INSERT INTO `specialties` (`speciality_id`, `name`) VALUES
(0, 'bez specjalności'),
(1, 'otolaryngologist'),
(4, 'orthopedist');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `PESEL` varchar(11) NOT NULL,
  `firstname` varchar(20) NOT NULL,
  `lastname` varchar(30) NOT NULL,
  `sex` varchar(1) NOT NULL,
  `email` varchar(40) NOT NULL,
  `password` varchar(100) NOT NULL,
  `privilege` int(11) DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `PESEL`, `firstname`, `lastname`, `sex`, `email`, `password`, `privilege`) VALUES
(23, '12345678978', 'rtrt', 'rtrt', 'M', 'bloc4488@gmail.com', '$2a$11$5nzwnycKXJNWQcsRi.0YrOGxOCDDKWSQoPGzkVXStKvLIh39dTape', 2),
(26, '00000000000', 'admin', 'admin', 'M', 'admin@gmail.com', '$2a$11$QBhwLewwucokaH5B6HkSpOlMxkGgXy2bE4RnytKzumjcLhlat5kne', 0),
(28, '02134569877', 'Raman', 'Priv', 'M', '1@gmail.com', '$2a$11$QBhwLewwucokaH5B6HkSpOlMxkGgXy2bE4RnytKzumjcLhlat5kne', 2),
(29, '17584963515', 'Bartosz', 'Nowak', 'M', 'b.nowak@gmail.com', '$2a$11$gVR8uNFyUSoYssj7g7TOnu8nLH788oPiLclHnG4rXaqJrqXzWBJ9.', 2),
(30, '12345678914', 'Raman', 'Skuratovich', 'M', '262703@gmail.com', '$2a$11$K2AIp66vEw73tdZA79rGNOpm2rlofp70CC5oPHao7uQPO77rLg8NO', 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `doctors`
--
ALTER TABLE `doctors`
  ADD PRIMARY KEY (`id`),
  ADD KEY `PESEL` (`user_id`),
  ADD KEY `speciality_id` (`speciality_id`);

--
-- Indexes for table `doctor_notes`
--
ALTER TABLE `doctor_notes`
  ADD PRIMARY KEY (`doctor_note_id`),
  ADD KEY `reservation_id` (`reservation_id`);

--
-- Indexes for table `grades_and_comments`
--
ALTER TABLE `grades_and_comments`
  ADD PRIMARY KEY (`comment_id`),
  ADD KEY `reservation_id` (`reservation_id`);

--
-- Indexes for table `reservations`
--
ALTER TABLE `reservations`
  ADD PRIMARY KEY (`reservation_id`),
  ADD KEY `doctor_id` (`doctor_id`),
  ADD KEY `patient_id` (`patient_id`);

--
-- Indexes for table `specialties`
--
ALTER TABLE `specialties`
  ADD PRIMARY KEY (`speciality_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `doctors`
--
ALTER TABLE `doctors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `doctor_notes`
--
ALTER TABLE `doctor_notes`
  MODIFY `doctor_note_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `grades_and_comments`
--
ALTER TABLE `grades_and_comments`
  MODIFY `comment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `reservations`
--
ALTER TABLE `reservations`
  MODIFY `reservation_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `specialties`
--
ALTER TABLE `specialties`
  MODIFY `speciality_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `doctors`
--
ALTER TABLE `doctors`
  ADD CONSTRAINT `doctors_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  ADD CONSTRAINT `doctors_ibfk_2` FOREIGN KEY (`speciality_id`) REFERENCES `specialties` (`speciality_id`) ON DELETE CASCADE;

--
-- Constraints for table `doctor_notes`
--
ALTER TABLE `doctor_notes`
  ADD CONSTRAINT `doctor_notes_ibfk_1` FOREIGN KEY (`reservation_id`) REFERENCES `reservations` (`reservation_id`) ON DELETE CASCADE;

--
-- Constraints for table `grades_and_comments`
--
ALTER TABLE `grades_and_comments`
  ADD CONSTRAINT `grades_and_comments_ibfk_1` FOREIGN KEY (`reservation_id`) REFERENCES `reservations` (`reservation_id`) ON DELETE CASCADE;

--
-- Constraints for table `reservations`
--
ALTER TABLE `reservations`
  ADD CONSTRAINT `reservations_ibfk_1` FOREIGN KEY (`doctor_id`) REFERENCES `users` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `reservations_ibfk_2` FOREIGN KEY (`patient_id`) REFERENCES `users` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
