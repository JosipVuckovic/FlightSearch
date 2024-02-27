<template>
  <div>

  </div>

  <div class="flight-search">
    <h1>Flight Search</h1>
    <p>This component demonstrates fetching data from the server.</p>

    <div v-if="loading" class="loading">
    </div>

    <div v-if="post" class="content">
      <table>
        <thead>
        <tr>
          <th>Data</th>         
        </tr>
        </thead>
        <tbody>
        <tr v-for="forecast in post.data">
          <td>{{ forecast }}</td>          
        </tr>      
        </tbody>
      </table>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

type Response = { 
  meta: object,
  data: object,
  dictionaries: object
};

interface Data {
  loading: boolean,
  post: null | Response
}

export default defineComponent({
  data(): Data {
    return {
      loading: false,
      post: null
    };
  },
  created() {
    // fetch the data when the view is created and the data is
    // already being observed
    this.fetchData();
  },
  watch: {
    // call again the method if the route changes
    '$route': 'fetchData'
  },
  methods: {
    fetchData(): void {
      this.post = null;
      this.loading = true;

      fetch('flightsearch')
          .then(r => r.json())
          .then(json => {
            this.post = json as Response;
            this.loading = false;
            return;
          });
    }
  },
});
</script>

<style scoped>

table {
  margin-left: auto;
  margin-right: auto;
}
</style>