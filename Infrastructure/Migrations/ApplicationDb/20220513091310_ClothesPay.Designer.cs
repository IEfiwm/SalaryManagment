﻿// <auto-generated />
using System;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations.ApplicationDb
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220513091310_ClothesPay")]
    partial class ClothesPay
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Domain.Entities.Data.Imported", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .UseIdentityColumn();

                    b.Property<string>("Absence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BasicOverTimePay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bonuses")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChildrenRightPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClothesPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContinuousBaseSalaryAndBaseYears")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContinuousBasicRightsToHousingAndChildrenRights")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Daily")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DailyPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Debt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DelayedSupplementaryInsuranceDeduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DelayedTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DelayedTransportationPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Deposit5Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disparity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DurationOperation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployerShareInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FamilyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FestalPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodAndHousingRight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FoodTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoodPerformance10Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HelpPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolidayworkingPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolidayworkingTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseRightPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImpurePerformance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IncludedAndNotIncluded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InstitutionaLoan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insurance23Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insurance30Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insurance7Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InsurancePay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Insured")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Leave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LifeAndAccidents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissionPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MissionTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Month")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Monthly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonthlyBenefits")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonthlyPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonthlyWagesAndBenefitsIncluded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NightworkingPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NightworkingTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NonContinuousIncluded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NonContinuousIncludedNotIncluded")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NonDependentSupplementaryInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberChildren")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other01")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Other02")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OtherDeductions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OverHead")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OvertimePay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OvertimeworkingPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OvertimeworkingTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartPaymentRecieved")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PerformancePay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PreviousReceipt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProjectRef")
                        .HasColumnType("bigint");

                    b.Property<string>("Pure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PureIncome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecievedRemainedPastMonth")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RemainedGoodPerformance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RemainedInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReturnGoodPerformance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReturnInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RewardPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RewardTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SamanLoan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sanavat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeveranceDaily")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeveranceMonthly")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShiftWorkPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShiftWorkTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SumDeductions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SumSalaryAndBenefit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SumWithInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplementaryInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplementaryInsuranceDeduction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplementaryInsuranceForDependents")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SupplementaryInsuranceSupervisor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Taxation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxationPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Total")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalBonusesAndLeave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalBonusesAndSanavatAndLeaveAndOverHead")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TotalWithoutBonusesAndLeave")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransportationPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnemploymentInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VAT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValueAddedAggregates")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ValueAddedInsurance")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WelfareAllowancePay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WelfareCostPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerRight")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkerRightPay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Year")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YearsPay")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .IsClustered();

                    b.ToTable("TbImported", "Data");
                });
#pragma warning restore 612, 618
        }
    }
}
