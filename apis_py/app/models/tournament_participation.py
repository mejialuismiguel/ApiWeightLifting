from sqlalchemy import Column, Integer, ForeignKey
from sqlalchemy.orm import relationship
from app.database import Base

class TournamentParticipation(Base):
    __tablename__ = "tournament_participation"

    id = Column(Integer, primary_key=True, index=True)
    athlete_id = Column(Integer, ForeignKey("athlete.id"), nullable=False)
    tournament_id = Column(Integer, ForeignKey("tournament.id"), nullable=False)
    weight_category_id = Column(Integer, ForeignKey("weight_category.id"), nullable=False)

    athlete = relationship("Athlete")
    tournament = relationship("Tournament")
    weight_category = relationship("WeightCategory")