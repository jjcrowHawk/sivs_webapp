const modalViviendaInfo = {
    props: {
        inspeccion_id: String,
        id: Number,
        address: String,
        location: String,
        elevation: String,
        inspection_date: String,
        inspector: String,
    },
    watch: {
        /*id_data: function (newVal, oldVal) {
            this.getVulnerabilityIndex();
        }*/
    },
    data() {
        return {
            inspeccion_id_data: "",
            id_data: -1,
            address_data: "",
            location_data: "",
            elevation_data: "",
            inspection_date_data: "",
            inspector_data: "",
        }
    },
    methods: {
        redirectToPage() {
            console.log("closing_modal");
            //this.$parent.$data["showModal"] = false;
            //$('#componente_modal_info').remove();
            $('#modal-template').remove();
            /*if (this.url_redirect != "") {
                location.href = this.url_redirect;
            }
            else {
                this.$parent.showModal = false;
            }*/
        },
        getFichaInfo() {
            var instance = this;
            $.get("/Home/GetFichaInformation/" + this.id_data, function (data, status) {
                if (status == "success") {
                    console.log(data);
                    var fichaDate = new Date(parseInt(data.fecha_inspeccion.replace(/\/Date\((-?\d+)\)\//, '$1')));
                    instance.inspection_date_data = fichaDate.toLocaleDateString("en-US");//`${fichaDate.getMonth()} - ${fichaDate.getUTCDay()} - ${fichaDate.getFullYear()}`;
                    instance.inspector_data = data.inspector;
                    instance.getRespuestasInfo(data.id);
                }
                else {
                    console.log("Error de consulta ficha");
                }
            })
        },
        getRespuestasInfo(fichaId) {
            var instance = this;
            $.get("/Home/GetRespuestasFicha/" + fichaId, function (data, status) {
                if (status == "success") {
                    console.log(data);

                }
                else {
                    console.log("Error de consulta respuesta");
                }
            })
        },
        /*getVulnerabilityIndex() {
            var instance = this;
            $.get("/Home/GetViviendaVulnerabilityScore/" + this.id_data, function (data, status) {
                //alert("Got RESPONSE!!! " + JSON.stringify(data) + "with status: " + status);
                instance.setScores(data.puntaje, data.puntaje_porcentaje);
                //this.score_data = data.puntaje;
                //this.score_weight_data = data.puntaje_porcentaje;
                //console.log(this.score_weight_data);
            })
        },
        setScores(puntaje, puntaje_porcentaje) {
            this.score_data = puntaje;
            this.score_weight_data = "% " + puntaje_porcentaje;
        },*/
        getData() {
            this.inspeccion_id_data = this.inspeccion_id;
            this.id_data = this.id;
            this.address_data = this.address;
            this.location_data = this.location;
            this.elevation_data = this.elevation;
            this.inspection_date_data = this.inspection_date;
            this.inspector_data = this.inspector;
        }

    },
    created() {

    },
    mounted() {
        this.getData();
        this.getFichaInfo();
        //this.getVulnerabilityIndex()
    },
    template: `
    <!-- template for the modal component -->
    <div id="modal-template">
        <transition name="modal">
            <div class="modal-mask">
            <div class="modal-wrapper">
                <div class="modal-container">
                <div class="modal-header" style="text-align:center;background-color:#307FE2">
                   <h3 slot="header" style="margin:auto; color:white; font-weight: bolder;">
                     Building Information
                    </h3>
                </div>
                <div class="modal-body">
                   <h4>Inspection Information</h4>
                    <hr />
                    <table>
                        <tr>
                            <td class="td_label">
                                ID:
                            </td>
                            <td class="">
                                {{this.inspeccion_id_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label">
                                Address:
                            </td>
                            <td>
                                {{this.address_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" style="text-decoration: underline;">
                                Location:
                            </td>
                            <td>
                                {{this.location_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" style="text-decoration: underline;">
                                Elevation:
                            </td>
                            <td>
                                {{this.elevation_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" style="text-decoration: underline;">
                                Inspection Date:
                            </td>
                            <td>
                                {{this.inspection_date_data}}
                            </td>
                        </tr>
                        <tr>
                            <td class="td_label" style="text-decoration: underline;">
                                Inspector:
                            </td>
                            <td>
                                {{this.inspector_data}}
                            </td>
                        </tr>
                    </table> 
                </div>
                <div class="modal-footer">
                    <button class="modal-default-button btn btn-primary btn-sm" @click="redirectToPage">
                        Close
                    </button>
                </div>
                </div>
            </div>
            </div>
        </transition>
    </div>
    `
}

/* Variable contenedora de la instancia del componente telefono*/
var contenedorModalViviendaInfo = new Vue({
    el: '#vivienda_modal_info',
    data: {
        showModal: false,
        fichasData: []
    },
    components: {
        'modal-vivienda-info': modalViviendaInfo
    },
    methods: {
    }
})