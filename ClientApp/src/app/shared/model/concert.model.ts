import { Ticket } from "./ticket.model";


export class Concert {
  Id: number;
  TourName: string;
  Artist: string;
  Stage: string;
  Picture: string;
  EventDate: Date;
  NumberTicketsAvailable: number;
  TicketPrice: number;
  Tickets: Ticket[];

}
