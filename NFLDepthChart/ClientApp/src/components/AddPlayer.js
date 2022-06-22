import React, { Component } from 'react';
import { ApiService } from '../services/ApiService';
import { useNavigate } from 'react-router-dom';

export class AddPlayer extends Component {
    static displayName = AddPlayer.name;

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
                    <label htmlFor="usr">Player Number:</label>
                    <input type="text" className="form-control"  id="Number" value={this.state.PlayerDetails.Number} onChange={(e) => this.handleChange(e, 'Number')} ></input>
                </div>
                <div className="form-group">
                    <label htmlFor="usr">Player Name:</label>
                    <input type="text" className="form-control" id="PlayerName" value={this.state.PlayerDetails.Name} onChange={(e) => this.handleChange(e, 'Name')} ></input>
                </div>
                <div className="form-group">
                    <label htmlFor="usr">Position:</label>
                    <input type="text" className="form-control" id="Postion" value={this.state.PlayerDetails.Position} onChange={(e) => this.handleChange(e, 'Position')} ></input>
                </div>
                <div className="form-group">
                    <label htmlFor="usr">Position Depth:</label>
                    <input type="text" className="form-control" id="PositionDepth" value={this.state.PlayerDetails.PositionDepth} onChange={(e) => this.handleChange(e, 'PositionDepth')} ></input>
                </div>
                <button className="btn btn-primary mt-2" onClick={() => this.addPlayerDetails()}>Save</button>
            </div>
        );
    }

    handleChange(event, id) {

        this.state.PlayerDetails = { ...this.state.PlayerDetails };

        this.state.PlayerDetails[id] = event.target.value;

        this.setState(this.state.PlayerDetails);
    }

    routeChange = () => {
        let path = '/';
        let history = useNavigate();
        history.push(path);
    }

    addPlayerDetails() {
        var apiService = new ApiService();
        apiService.ApiPost('depthchart/AddPlayer', this.state.PlayerDetails);

        window.location.href = '/';
    }
}
