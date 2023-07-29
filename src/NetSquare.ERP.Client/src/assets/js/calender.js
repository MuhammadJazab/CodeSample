
document.addEventListener("DOMContentLoaded", function () {
  var calendarEl = document.getElementById("my_calendar");

  var calendar = new FullCalendar.Calendar(calendarEl, {
    timeZone: "UTC",
    initialView: "dayGridMonth",
    events: "https://fullcalendar.io/demo-events.json",
    editable: true,
    selectable: true,
  });

  calendar.render();
});
