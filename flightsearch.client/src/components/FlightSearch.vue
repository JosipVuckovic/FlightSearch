<template xmlns="http://www.w3.org/1999/html">
  <header>

    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Flight Search</title>
    <link
        href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.0/dist/css/bootstrap.min.css"
        rel="stylesheet"
        integrity="sha384-KyZXEAg3QhqLMpG8r+8fhAXLRk2vvoC2f3B09zVXn8CA5QIVfZOJ3BCsw2P0p/We"
        crossorigin="anonymous"
    />
    <link
        rel="stylesheet"
        href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.5.0/font/bootstrap-icons.css"
    />
    <link rel="stylesheet" href="./style.css"/>


    <div class="container-sm">
      <div class="my-2 card">
        <div class="card-body">
          <h5 class="card-title">Locations</h5>
          <div class="row">
            <div class="col-sm">
              <div class="mb-2">
                <label id="origin-label" for="origin-input" class="form-label"
                >Origin</label
                >
                <div class="input-group">
                  <span class="input-group-text"
                  ><i class="bi-pin-map"></i>
                  </span>
                  <input
                      type="text"
                      class="form-control"
                      id="origin-input"
                      placeholder="Location"
                      aria-describedby="origin-label"
                      required
                      v-model="originLocationCode"
                      :class="{'input--error':!originLocationCode}"
                  />
                  <datalist id="origin-options"></datalist>
                </div>
              </div>
            </div>
            <div class="col-sm">
              <div class="mb-2">
                <label
                    id="destination-label"
                    for="destination-input"
                    class="form-label"
                >Destination</label
                >
                <div class="input-group">
                  <span class="input-group-text"
                  ><i class="bi-pin-map-fill"></i>
                  </span>
                  <input
                      type="text"
                      class="form-control"
                      id="destination-input"
                      placeholder="Location"
                      aria-describedby="destination-label"
                      required
                      v-model="destinationLocationCode"
                      :class="{'input--error':!destinationLocationCode}"
                  />
                  <datalist id="destination-options"></datalist>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="row">
        <div class="mb-2 col">
          <div class="h-100 card">
            <div class="card-body">
              <h5 class="card-title">Dates</h5>
              <div id="departure-date" class="mb-2">
                <label
                    id="departure-date-label"
                    for="departure-date-input"
                    class="form-label"
                >Departure date</label
                >
                <div class="input-group">
                  <span class="input-group-text"
                  ><i class="bi-calendar"></i>
                  </span>
                  <input
                      type="date"
                      class="form-control"
                      id="departure-date-input"
                      aria-describedby="departure-date-label"
                      required
                      v-model="departureDate"
                      :class="{'input--error':!departureDate}"
                  />
                </div>
              </div>
              <div id="return-date" class="mb-2">
                <label
                    id="return-date-label"
                    for="return-date-input"
                    class="form-label"
                >Return date</label
                >
                <div class="input-group">
                  <span class="input-group-text"
                  ><i class="bi-calendar-fill"></i>
                  </span>
                  <input
                      type="date"
                      class="form-control"
                      id="return-date-input"
                      aria-describedby="return-date-label"
                      v-model="returnDate"
                  />
                </div>
              </div>
              <div class="mb-2">
                <div class="input-group">
                  <label class="input-group-text"
                  >Price</label
                  >
                  <input
                      type="number"
                      min="0"
                      class="form-control"
                      aria-describedby="infants-label"
                      required
                      v-model="maxPrice"
                      :class="{'input--error':!maxPrice}"
                  />
                </div>
              </div>
              <div class="mb-2">
                <div class="input-group">
                  <label class="input-group-text"
                  >Currency Code</label
                  >
                  <input
                      type="text"
                      class="form-control"
                      list="origin-options"
                      id="origin-input"
                      placeholder="three letter code"
                      required
                      v-model="currencyCode"
                      :class="{'input--error':!currencyCode}"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="mb-2 col">
          <div class="h-100 card">
            <div class="card-body">
              <h5 class="card-title">Details</h5>
              <div class="mb-2">
                <label
                    id="travel-class-label"
                    for="travel-class-select"
                    class="form-label"
                >Travel class</label
                >
                <select
                    class="form-select"
                    id="travel-class-select"
                    aria-describedby="travel-class-label"
                    v-model="travelClass"
                >
                  <option value="ECONOMY">Economy</option>
                  <option value="PREMIUM_ECONOMY">Premium Economy</option>
                  <option value="BUSINESS">Business</option>
                  <option value="FIRST">First</option>
                </select>
              </div>
              <label class="form-label">Passengers</label>
              <div class="mb-2">
                <div class="input-group">
                  <label for="adults-input" class="input-group-text"
                  >Adults</label
                  >
                  <input
                      type="number"
                      min="0"
                      class="form-control"
                      id="adults-input"
                      aria-describedby="adults-label"
                      required
                      v-model="adults"
                      :class="{'input--error':!adults}"
                  />
                </div>
                <span id="adults-label" class="form-text"
                >12 years old and older</span
                >
              </div>
              <div class="mb-2">
                <div class="input-group">
                  <label for="children-input" class="input-group-text"
                  >Children</label
                  >
                  <input
                      type="number"
                      min="0"
                      class="form-control"
                      id="children-input"
                      aria-describedby="children-label"
                      v-model="children"
                  />
                </div>
                <span id="children-label" class="form-text"
                >2 to 12 years old</span
                >
              </div>
              <div class="mb-2">
                <div class="input-group">
                  <label for="infants-input" class="input-group-text"
                  >Infants</label
                  >
                  <input
                      type="number"
                      min="0"
                      class="form-control"
                      id="infants-input"
                      aria-describedby="infants-label"
                      v-model="infants"
                  />
                </div>
                <span id="infants-label" class="form-text"
                >Up to 2 years old</span
                >
              </div>

            </div>
          </div>
        </div>
      </div>
      <button id="search-button" class="w-100 btn btn-primary" @click="fetchData">Search</button>
    </div>

  </header>

  <main>

    <div v-if="post" class="content">
      <table>
        <thead>
        <tr>
          <th>Departure Date</th>
          <th>Departure Airport</th>
          <th>Dep Carrier</th>
          <th>Arrival Date</th>
          <th>Arrival Airport</th>
          <th>Arr Carrier</th>
          <th>Planes No.</th>
          <th>Passenger No.</th>
          <th>Total price</th>
          <th>Currency</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="flight in post" :key="flight.departureDate">
          <td>{{ flight.departureDate }}</td>
          <td>{{ flight.departureAirport }}</td>
          <td>{{ flight.departureCarrier }}</td>
          <td>{{ flight.arrivalDate }}</td>
          <td>{{ flight.arrivalAirport }}</td>
          <td>{{ flight.arrivalCarrier }}</td>
          <td>{{ flight.numberOfPlaneChanges }}</td>
          <td>{{ flight.numberOfPassengers }}</td>
          <td>{{ flight.totalPrice }}</td>
          <td>{{ flight.currency }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>

<script lang="ts">
import {defineComponent} from 'vue';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

type FlightSearchViewModels = {
  departureAirport: string;
  departureDate: string;
  departureCarrier: string;
  arrivalAirport: string;
  arrivalDate: string;
  arrivalCarrier: string;
  numberOfPlaneChanges: string;
  numberOfPassengers: string;
  totalPrice: string;
  currency: string;
}[];

interface Data {
  originLocationCode: string,
  destinationLocationCode: string,
  departureDate: string,
  returnDate: string | null,
  adults: number,
  children: number,
  infants: number,
  travelClass: string | null,
  nonStop: boolean,
  currencyCode: string | null,
  maxPrice: number | null,
  max: number,
  loading: boolean,
  post: null | FlightSearchViewModels
}

export default defineComponent({
  data(): Data {
    return {
      travelClass: null,
      adults: 0,
      children: 0,
      currencyCode: null,
      departureDate: "",
      destinationLocationCode: "",
      infants: 0,
      max: 0,
      maxPrice: 0,
      nonStop: false,
      originLocationCode: "",
      returnDate: null,
      loading: false,
      post: null
    };
  },

  methods: {
    fetchData(): void {
      this.post = null;
      this.loading = true;

      if (this.originLocationCode == null || this.originLocationCode == '' ||
          this.destinationLocationCode == null   || this.destinationLocationCode == '' ||
          this.departureDate == null || this.departureDate == '' ||
          this.currencyCode == null || this.currencyCode == '' ||
          this.maxPrice == null || this.maxPrice == 0 ||
          this.adults == null || this.adults == 0
      )
      {
        toast.error('Required fields not filled');
        return;
      }
      
      fetch('flightsearch', {
        method: 'post',
        headers: new Headers({
          'Content-type': 'application/json; charset=UTF-8'
        }),
        body: JSON.stringify({
          "originLocationCode": this.originLocationCode,
          "destinationLocationCode": this.destinationLocationCode,
          "departureDate": this.departureDate,
          "returnDate": this.returnDate,
          "travelClass": this.travelClass,
          "adults": this.adults,
          "children": this.children,
          "infants": this.infants,
          "nonStop": this.nonStop,
          "currencyCode": this.currencyCode,
          "maxPrice": this.maxPrice,
          "max": 250
        })
      }).then(r => r.json())
          .then(json => {
            this.post = json;
            this.loading = false;
            return;
          }).catch(e => {          
          toast.error('Bad request :(');
        });
    }
  },
});
</script>

<style scoped>

th {
  font-weight: bold;
}

tr:nth-child(even) {
  background: #F2F2F2;
}

tr:nth-child(odd) {
  background: #FFF;
}

th, td {
  padding-left: .5rem;
  padding-right: .5rem;
  border: 1px solid black;
}

.weather-component {
  text-align: center;
}

table {
  margin-left: auto;
  margin-right: auto;
}

.input--error{
  border-color:red;
}
</style>