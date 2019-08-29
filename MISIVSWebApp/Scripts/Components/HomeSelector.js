const homeSelector = {
    data() {
        return {
            sectores: {},
            selected: '',
            home_selected: null,
            showInfo: false,
            vivenda: {},
            score: "Loading info...",
            score_weight: "Loading info...",
        }
    },
    methods: {
        setHome() {
        },
        showBuildingInfo() {
            showViviendaInfo(this.vivienda);
        },
        showHomeInfo() {
            console.log("SHOWING INFOOOO");
            this.vivienda = this.home_selected;
            this.showInfo = false;
            this.showInfo = true;
            var instance = this;
            $.get("/Home/GetViviendaVulnerabilityScore/" + this.vivienda.id, function (data, status) {
                instance.score = data.puntaje;
                instance.score_weight = data.puntaje_porcentaje + " %";
            });
        }
    },
    mounted() {
        this.showInfo = false;
        this.home_selected = null;
        this.selected = '';
    },
    template: `
        <div>
        <div class="row" style="background-color:#307FE2; padding:1%;margin:2%">
            <div class="col-md-2">
                <div class="abc">
                    <p class="center" style="color:White">Choose a sector: </p>
                </div>
            </div>
            <select v-model='selected' class="col-md-2 selector" id="selector">
                <option v-for="sector in Object.keys(sectores)" :value="sector" :key="sector">
                    {{ sector }}
                </option>
            </select>
            <div class="col-md-2">
                <div class="abc">
                    <p class="center" style="color:White">Choose a Building: </p>
                </div>
            </div>
            <select v-if="selected != ''" v-model='home_selected' :change="setHome()" class="col-md-2 selector" id="h_selector">
                <option v-for="home in sectores[selected]" :value="home" >
                    {{ home.inspeccion_id }}
                </option>
            </select>
            <div class="col-md-2" style="margin-left:8%">
            <button v-if="home_selected != null && selected != ''" class="button" v-on:click="showHomeInfo()"> Consult </button>
            </div>
        </div>
        <div v-if="showInfo" style="margin-top: 8%">
                   <h4>Vulnerability Analysis</h4>
                    <hr />
                    <table>
                        <tr>
                            <td class="td_label">
                                ID:
                            </td>
                            <td class="">
                                {{this.vivienda.inspeccion_id}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label">
                                Address:
                            </td>
                            <td>
                                {{this.vivienda.ubicacion}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" style="text-decoration: underline;">
                                Vulnerability score:
                            </td>
                            <td>
                                {{this.score}}
                            </td>
                            <td class="td_label" style="text-decoration: underline;">
                                Vulnerability percentage:
                            </td>
                            <td>
                                {{this.score_weight}}
                            </td>
                        </tr>
                    </table> 
                    <div class="" style="">
                        <button class="button" v-on:click="showBuildingInfo()"> View Home Information </button>
                    </div>
        </div>
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