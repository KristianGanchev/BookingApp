﻿using BookingApp.Domain.Abstractions;
using BookingApp.Domain.Shared;

namespace BookingApp.Domain.Apartments;

public sealed class Apartment(
    Guid id,
    Name name,
    Description description,
    Address address,
    Money price,
    Money cleaningFee,
    List<Amenity> amenities) 
    : Entity(id)
{
    public Name Name { get; private set; } = name;

    public Description Description { get; private set; } = description;

    public Address Address { get; private set; } = address;

    public Money Price { get; private set; } = price;

    public Money CleaningFee { get; private set; } = cleaningFee;

    public DateTime? LastBookedOnUtc { get; internal set; }

    public List<Amenity> Amenities { get; set; } = amenities;
}
