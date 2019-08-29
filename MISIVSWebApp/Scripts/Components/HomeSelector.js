const homeSelector = {
    data() {
        return {
            sectores: {},
            selected: '',
        }
    },
    methods: {

    },
    mounted() {

    },
    template: `
        <div class="row">
            <div class="col-md-2">
                <div class="abc">
                    <p class="center">Choose a sector:</p>
                </div>
            </div>
            <select v-model='selected' class="col-md-2 selector" id="selector">
                <option v-for="sector in Object.keys(sectores)" :value="sector" :key="sector">
                    {{ sector }}
                </option>
            </select>
            <div class="col-md-2">
                <div class="abc">
                    <p class="center">Choose a Building:</p>
                </div>
            </div>
            <select v-if="selected != ''" v-model='selected' class="col-md-2 selector" id="h_selector">
                <option v-for="home in sectores[selected]" :value="home.inspeccion_id" :key="home.inspeccion_id">
                    {{ home.inspeccion_id }}
                </option>
            </select>
        </div>
    `

}

var contenedorHomeSelector = new Vue({
    el: '#home_selector_info',
    data: {
        //showModal: false,
        fichasData: []
    },
    components: {
        'home-selector': homeSelector
    },
    methods: {
    }
})