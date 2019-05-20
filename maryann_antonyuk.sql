-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: May 20, 2019 at 01:05 AM
-- Server version: 5.7.25
-- PHP Version: 7.3.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `maryann_antonyuk`
--
CREATE DATABASE IF NOT EXISTS `maryann_antonyuk` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `maryann_antonyuk`;

-- --------------------------------------------------------

--
-- Table structure for table `Client`
--

CREATE TABLE `Client` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Client`
--

INSERT INTO `Client` (`id`, `name`, `stylist_id`) VALUES
(1, 'maryana', 0),
(2, 'andrii', 0),
(3, 'maryana', 1),
(6, 'andrii', 2);

-- --------------------------------------------------------

--
-- Table structure for table `Specialty`
--

CREATE TABLE `Specialty` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Specialty`
--

INSERT INTO `Specialty` (`id`, `name`) VALUES
(1, 'color'),
(2, 'cut');

-- --------------------------------------------------------

--
-- Table structure for table `Specialty_Stylist`
--

CREATE TABLE `Specialty_Stylist` (
  `specialty_id` int(11) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Specialty_Stylist`
--

INSERT INTO `Specialty_Stylist` (`specialty_id`, `stylist_id`) VALUES
(1, 1),
(1, 2),
(2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `Stylist`
--

CREATE TABLE `Stylist` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `Stylist`
--

INSERT INTO `Stylist` (`id`, `name`) VALUES
(1, 'zverev'),
(2, 'marina zvereva');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `Client`
--
ALTER TABLE `Client`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `Specialty`
--
ALTER TABLE `Specialty`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `Specialty_Stylist`
--
ALTER TABLE `Specialty_Stylist`
  ADD UNIQUE KEY `speciality_stylist_ids` (`specialty_id`,`stylist_id`) USING BTREE;

--
-- Indexes for table `Stylist`
--
ALTER TABLE `Stylist`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `Client`
--
ALTER TABLE `Client`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `Specialty`
--
ALTER TABLE `Specialty`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `Stylist`
--
ALTER TABLE `Stylist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
