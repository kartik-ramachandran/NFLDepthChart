import React, { Component } from 'react';
import { ApiService } from '../services/ApiService';

export class RemovePlayer extends Component {
    static displayName = RemovePlayer.name;
    constructor(props) {
        super(props);

        this.state = {
            PlayerDetails: {
                Number: '',
                Name: '',
                Position: '',
                PositionDepth: ''
            }
        };
    }


    render() {

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

                <button className="btn btn-primary mt-2" onClick={() => this.removePlayer()}>Remove Player</button>
            </div>
        );
    }

    handleChange(event, id) {

        this.state.PlayerDetails = { ...this.state.PlayerDetails };

        this.state.PlayerDetails[id] = event.target.value;

        this.setState(this.state.PlayerDetails);
    }

    removePlayer() {
        var apiService = new ApiService();

        apiService.ApiPost('depthchart/RemovePlayer', this.state.PlayerDetails);

        window.location.href = '/';
    }
}
