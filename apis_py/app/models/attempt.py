from sqlalchemy import Column, Integer, Float, String, ForeignKey
from sqlalchemy.orm import relationship
from app.database import Base

class Attempt(Base):
    __tablename__ = "attempt"

    id = Column(Integer, primary_key=True, index=True)
    participation_id = Column(Integer, ForeignKey("tournament_participation.id"), nullable=False)
    attempt_number = Column(Integer, nullable=False)
    type = Column(String(10), nullable=False)
    weight_lifted = Column(Float, nullable=False)
    success = Column(Integer, nullable=False)

    participation = relationship("TournamentParticipation")