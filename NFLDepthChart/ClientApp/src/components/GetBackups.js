import React, { Component } from 'react';
import { ApiService } from '../services/ApiService';

export class GetBackups extends Component {
    static displayName = GetBackups.name;
    constructor(props) {
        super(props);

        this.state = {
            BackupPlayers: [],
            PlayerDetails: {
                Number: '',
                Name: '',
                Position: '',
                PositionDepth: ''
            }
        };
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : GetBackups.renderDepthChart(this.state.BackupPlayers);

        return (
            <div>

                <div className="form-group">
                    <label htmlFor="usr">Player Name:</label>
                    <input type="text" className="form-control" id="PlayerName" value={this.state.PlayerDetails.Name} onChange={(e) => this.handleChange(e, 'Name')} ></input>
                </div>
                <div className="form-group">
                    <label htmlFor="usr">Position:</label>
                    <input type="text" className="form-control" id="Postion" value={this.state.PlayerDetails.Position} onChange={(e) => this.handleChange(e, 'Position')} ></input>
                </div>

                <button className="btn btn-primary mt-2" onClick={() => this.fetchBackups()}>Fetch Backup(s)</button>

                <div className="mt-2">
                    {contents}
                </div>
            </div>
        );
    }

    static renderDepthChart(playerDetails) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {
                        playerDetails.map((players, i) => (
                            <tr key={players.number}>
                                <td>#{players.number} {players.name} - Position Depth #{players.positionDepth}</td>
                            </tr>
                            ))
                    }
                  
                </tbody>
            </table>
        );
    }


    handleChange(event, id) {

        this.state.PlayerDetails = { ...this.state.PlayerDetails };

        this.state.PlayerDetails[id] = event.target.value;

        this.setState(this.state.PlayerDetails);
    }

    async fetchBackups() {
        var apiService = new ApiService();
        var data = await apiService.ApiPost('depthchart/GetBackups', this.state.PlayerDetails);
        this.setState({ BackupPlayers: data })
    }

}
