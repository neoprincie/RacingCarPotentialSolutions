using System;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace TDDMicroExercises.TurnTicketDispenser.Tests;

public class TicketDispenserTests
{
    private readonly TurnNumberSequence _turnNumberSequence;

    public TicketDispenserTests()
    {
        _turnNumberSequence = new TurnNumberSequence();
    }
    
    [Fact]
    public void GetTurnTicket_GivenOneUse_ShouldReturnTicket()
    {
        var dispenser = new TicketDispenser(_turnNumberSequence);
        
        var ticket = dispenser.GetTurnTicket();
    
        ticket.TurnNumber.Should().Be(0);
    }

    [Fact]
    public void GetTurnTicket_GivenTwoTickets_ShouldReturnSequentialNumbers()
    {
        var dispenser = new TicketDispenser(_turnNumberSequence);
        
        var ticket1 = dispenser.GetTurnTicket();
        var ticket2 = dispenser.GetTurnTicket();
    
        ticket1.TurnNumber.Should().Be(0);
        ticket2.TurnNumber.Should().Be(1);
    }

    [Fact]
    public void GetTurnTicket_GivenTwoTicketDispensers_ShouldReturnSequentialNumbers()
    {
        var dispenser1 = new TicketDispenser(_turnNumberSequence);
        var dispenser2 = new TicketDispenser(_turnNumberSequence);

        var ticket1 = dispenser1.GetTurnTicket();
        var ticket2 = dispenser2.GetTurnTicket();

        ticket1.TurnNumber.Should().Be(0);
        ticket2.TurnNumber.Should().Be(1);
    }
}